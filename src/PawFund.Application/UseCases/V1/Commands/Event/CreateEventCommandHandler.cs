﻿
using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Exceptions;
using static PawFund.Domain.Exceptions.BranchException;
using static PawFund.Domain.Exceptions.EventException;

namespace PawFund.Application.UseCases.V1.Commands.Event;

public sealed class CreateEventCommandHandler : ICommandHandler<Command.CreateEventCommand>
{
    private readonly IRepositoryBase<PawFund.Domain.Entities.Branch, Guid> _branchRepository;
    private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dPUnitOfWork;
    private readonly IMediaService _mediaService;

    public CreateEventCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork, IMediaService mediaService)
    public CreateEventCommandHandler(IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork, IMediaService mediaService)
    {
        _branchRepository = branchRepository;
        _eventRepository = eventRepository;
        _efUnitOfWork = efUnitOfWork;
        _dPUnitOfWork = dPUnitOfWork;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(Command.CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.StartDate >= request.EndDate)
        {
            throw new EventDateException();
        }

        //check branch for event
        var branchId = await _dPUnitOfWork.BranchRepositories.GetAllBranchByAccountId(request.userId);

        Domain.Entities.Branch branch;
        if (branchId.Count != 0)
        {
            branch = await _branchRepository.FindByIdAsync(branchId[0]);
        }
        else
        {
            throw new BranchNotFoundOfStaffException(request.userId);
        }
        var branchId = await _dPUnitOfWork.BranchRepositories.GetAllBranchByAccountId(request.userId);

        Domain.Entities.Branch branch;
        if (branchId.Count != 0)
        {
            branch = await _efUnitOfWork.BranchRepository.FindByIdAsync(branchId[0]);
        }
        else
        {
            throw new BranchNotFoundOfStaffException(request.userId);
        }

        var uploadImages = await _mediaService.UploadImagesAsync(new List<IFormFile> { request.ThumbHeroUrl, request.ImagesUrl });

        if (branch != null || branch.IsDeleted != true)
        {
            //create new event
            var newEvent = Domain.Entities.Event.CreateEvent(request.Name, request.StartDate, request.EndDate, request.Description, request.MaxAttendees, branchId[0], uploadImages[0].ImageUrl, uploadImages[0].PublicImageId, uploadImages[1].ImageUrl, uploadImages[1].PublicImageId, DateTime.Now, DateTime.Now, false, null);
            newEvent.Status = Contract.Enumarations.Event.EventStatus.NotApproved;
            _eventRepository.Add(newEvent);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);

            //Return result
            return Result.Success(new Success(MessagesList.CreateEventSuccessfully.GetMessage().Code, MessagesList.CreateEventSuccessfully.GetMessage().Message));
        }
        else
        {
            throw new BranchException.BranchNotFoundException(branchId[0]);
        }
    }
}