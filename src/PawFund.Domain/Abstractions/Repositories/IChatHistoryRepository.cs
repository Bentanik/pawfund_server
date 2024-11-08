﻿using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Repositories;

public interface IChatHistoryRepository : IRepositoryBase<ChatHistory, Guid>
{
}