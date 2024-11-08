﻿using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Contract.Enumarations.VolunteerApplication;
using MediatR;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.VolunteerApplicationDetail
{ 
    public sealed class RejectVolunteerApplicationCommandHandler : ICommandHandler<Command.RejectVolunteerApplicationCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPublisher _publisher;

        public RejectVolunteerApplicationCommandHandler(IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher)
        {
            _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
            _efUnitOfWork = efUnitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.RejectVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            //change status application
            var existVolunteerApplication = await _volunteerApplicationDetailRepository.FindByIdAsync(request.detailId);
            existVolunteerApplication.UpdateVolunteerApplication(VolunteerApplicationStatus.Rejected, request.reason);
            await _efUnitOfWork.SaveChangesAsync();


            // Send email
            await Task.WhenAll(
               _publisher.Publish(new DomainEvent.RejectSendMail(Guid.NewGuid(),request.reason , existVolunteerApplication.Account.Email, existVolunteerApplication.EventActivity.Name), cancellationToken)
           );
            return Result.Success(new Success(MessagesList.RejectVolunteerApplicationSuccessfully.GetMessage().Code, MessagesList.RejectVolunteerApplicationSuccessfully.GetMessage().Message));
        }
    }
}
