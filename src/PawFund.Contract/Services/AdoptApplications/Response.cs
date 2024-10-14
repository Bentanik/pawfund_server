﻿using PawFund.Contract.DTOs.Adopt;
using PawFund.Contract.Enumarations.AdoptPetApplication;
namespace PawFund.Contract.Services.AdoptApplications;

public static class Response
{
    public record GetApplicationByIdResponse
        (Guid Id,
        DateTime? MeetingDate,
        string? ReasonReject,
        string Status,
        string Description,
        bool IsFinalized,
        GetApplicationByIdDTO.AccountDto Account,
        GetApplicationByIdDTO.CatDto Cat);

    public record GetAllApplicationResponse(List<GetAllApplicationsDTO.AdoptApplicationDTO> List);


}