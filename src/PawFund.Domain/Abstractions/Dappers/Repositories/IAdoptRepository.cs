﻿using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IAdoptRepository : IGenericRepository<AdoptPetApplication>
{
    Task<bool> HasAccountRegisterdWithPet(Guid accountId, Guid catId);
}

