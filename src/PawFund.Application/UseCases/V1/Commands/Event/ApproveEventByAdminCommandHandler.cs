﻿using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class ApproveEventByAdminCommandHandler : ICommandHandler<Command.ApprovedEventByAdmin>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public ApproveEventByAdminCommandHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork)
        {
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.ApprovedEventByAdmin request, CancellationToken cancellationToken)
        {
            //find event
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);

            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.Id);
            }

            existEvent.Status = Contract.Enumarations.Event.EventStatus.NotStarted;
            _eventRepository.Update(existEvent);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Approve event success");
        }
    }
}