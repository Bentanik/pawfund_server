﻿using Microsoft.Extensions.DependencyInjection;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Infrastructure.Dapper.Repositories;

namespace PawFund.Infrastructure.Dapper.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDapper(this IServiceCollection services)
        => services.AddTransient<IAccountRepository, AccountRepository>()
                    .AddTransient<IDPUnitOfWork, DPUnitOfWork>()
        .AddTransient<IAdoptRepository, AdoptRepository>()
        .AddTransient<ICatRepository, CatRepository>()
        .AddTransient<IBranchRepository, BranchRepository>()
        .AddTransient<IEventActivityRepository, EventActivityRepository>()
        .AddTransient<IEventRepository, EventRepository>()
        .AddTransient<IHistoryCatRepository, HistoryCatRepository>()
        .AddTransient<IRoleUser, RoleUser>()
        .AddTransient<IVolunteerApplicationDetail, VolunteerApplicationDetail>()
        .AddTransient<IProductRepository, ProductRepository>()
        .AddTransient<IDonationRepository, DonationRepository>();
}
