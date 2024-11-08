﻿using PawFund.Domain.Abstractions.Dappers.Repositories;

namespace PawFund.Domain.Abstractions.Dappers;

public interface IDPUnitOfWork
{
    IAccountRepository AccountRepositories { get; }
    IAdoptRepository AdoptRepositories { get; }
    ICatRepository CatRepositories { get; }
    IHistoryCatRepository HistoryCatRepositories { get; }
    IBranchRepository BranchRepositories { get; }
    IEventActivityRepository EventActivityRepositories { get; }
    IEventRepository EventRepository { get; }
    IHistoryCatRepository HistoryCatRepository { get; }
    IRoleUser RoleUserRepository { get; }
    IVolunteerApplicationDetail VolunteerApplicationDetailRepository { get; }
    IDonationRepository DonationRepository { get; }
    IChatHistoryRepository ChatHistoryRepository { get; }
    IMessageRepository MessageRepository { get; }
}
