﻿
using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.EventDTOs.Respone;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Contract.DTOs.EventDTOs.Respone.EventForUserDTO;

namespace PawFund.Application.UseCases.V1.Queries.Event
{
    public sealed class GetEventByIdQueryHandler : IQueryHandler<Query.GetEventByIdQuery, Success<Respone.EventResponse>>
    {
        private readonly IDPUnitOfWork _dPUnitOfWork;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IDPUnitOfWork dPUnitOfWork, IMapper mapper)
        {
            _dPUnitOfWork = dPUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<Respone.EventResponse>>> Handle(Query.GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var existEvent = await _dPUnitOfWork.EventRepository.GetByIdAsync(request.Id);
            if (existEvent != null || existEvent.IsDeleted == false)
            {
                //EventDTO eventDTO = new EventDTO()
                //{
                //    Id = request.Id,
                //    Name = existEvent.Name,
                //    Description = existEvent.Description,
                //    StartDate = existEvent.StartDate,
                //    EndDate = existEvent.EndDate,
                //    MaxAttendees = existEvent.MaxAttendees,
                //    Status = existEvent.Status.ToString(),
                //    ThumbHeroUrl = existEvent.ThumbHeroUrl,
                //    ImagesUrl = existEvent.ImagesUrl,
                //};
                //BranchDTO branchDTO = new BranchDTO()
                //{
                //    Id = existEvent.Branch.Id,
                //    Name = existEvent.Branch.Name,
                //    Description = existEvent.Branch.Description,
                //    District = existEvent.Branch.District,
                //    EmailOfBranch = existEvent.Branch.EmailOfBranch,
                //    NumberHome = existEvent.Branch.NumberHome,
                //    PhoneNumberOfBranch = existEvent.Branch.PhoneNumberOfBranch,
                //    PostalCode = existEvent.Branch.PostalCode,
                //    Province = existEvent.Branch.Province,
                //    StreetName = existEvent.Branch.StreetName,
                //    Ward = existEvent.Branch.Ward,
                //};

                var mapperEvent = _mapper.Map<EventForUserDTO.EventDTO>(existEvent);
                var result = new Respone.EventResponse(mapperEvent);
                return Result.Success(new Success<Respone.EventResponse>("", "", result));
            }
            else
            {
                throw new Domain.Exceptions.EventException.EventNotFoundException(request.Id);
            }
        }
    }
}
