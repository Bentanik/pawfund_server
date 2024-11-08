﻿using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.CatDTOs;
namespace PawFund.Contract.Services.Cats;

public static class Query
{
    public record GetCatByIdQuery(Guid Id, Guid AccountId) : IQuery<Success<Response.CatResponse>>;
    public record GetCats(int PageIndex,
        int PageSize,
        Filter.CatAdoptFilter FilterParams,
        string[] SelectedColumns) : IQuery<Success<PagedResult<CatDto>>>;

    public record GetCatsStaffQuery(int PageIndex,
      int PageSize,
      Filter.CatFilter FilterParams,
      string[] SelectedColumns) : IQuery<Success<PagedResult<CatDto>>>;
}
