﻿using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Contract.DTOs.Event.GetEventByIdDTO;
using static PawFund.Contract.DTOs.EventActivity.GetEventActivityByIdDTO;

namespace PawFund.Application.UseCases.V1.Queries.EventActivity
{
    public sealed class GetEventActivityByIdQueryHandler : IQueryHandler<Query.GetEventActivityByIdQuery, Respone.EventActivityResponse>
    {
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public GetEventActivityByIdQueryHandler(IDPUnitOfWork dPUnitOfWork)
        {
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result<Respone.EventActivityResponse>> Handle(Query.GetEventActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var existActivity = await _dPUnitOfWork.EventActivityRepositories.GetByIdAsync(request.Id);
            if (existActivity != null)
            {
                ActivityDTO activityDTO = new ActivityDTO()
                {
                    Id = request.Id,
                    Description = existActivity.Description,
                    Name = existActivity.Name,
                    Quantity = existActivity.Quantity,
                    StartDate = existActivity.StartDate,
                    Status = existActivity.Status
                };
                GetEventActivityByIdDTO.EventDTO eventDTO = new GetEventActivityByIdDTO.EventDTO()
                {
                    Id = existActivity.Event.Id,
                    Description = existActivity.Event.Description,
                    Name = existActivity.Event.Name,
                    StartDate = existActivity.Event.StartDate,
                    EndDate = existActivity.Event.EndDate,
                    MaxAttendees = existActivity.Event.MaxAttendees,
                    Status = existActivity.Event.Status.ToString()
                };
                var result = new Respone.EventActivityResponse(activityDTO, eventDTO);
                return Result.Success(result);
            }
            else
            {
                throw new Domain.Exceptions.EventActivityException.EventActivityNotFoundException(request.Id);
            }
        }
    }
}