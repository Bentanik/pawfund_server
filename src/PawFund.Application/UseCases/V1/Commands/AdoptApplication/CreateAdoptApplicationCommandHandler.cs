﻿using Microsoft.EntityFrameworkCore;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;


namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication;
public sealed class CreateAdoptApplicationCommandHandler : ICommandHandler<Command.CreateAdoptApplicationCommand>
{
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptApplicationRepository;
    private readonly IRepositoryBase<Account, Guid> _accountRepository;
    private readonly IRepositoryBase<PawFund.Domain.Entities.Cat, Guid> _catRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dbUnitOfWork;

    public CreateAdoptApplicationCommandHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptApplicationRepository, IRepositoryBase<Account, Guid> accountRepository, IRepositoryBase<PawFund.Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dbUnitOfWork)
    {
        _adoptApplicationRepository = adoptApplicationRepository;
        _accountRepository = accountRepository;
        _catRepository = catRepository;
        _efUnitOfWork = efUnitOfWork;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<Result> Handle(Command.CreateAdoptApplicationCommand request, CancellationToken cancellationToken)
    {
        var userFound = await _accountRepository.FindByIdAsync(request.AccountId);
        if (userFound == null)
        {
            throw new AuthenticationException.UserNotFoundByIdException(request.AccountId);
        }
        var catFound = await _catRepository.FindByIdAsync(request.CatId);
        if(catFound == null)
        {
            throw new CatException.CatNotFoundException(request.CatId);
        }
        var hasAccountRegisteredWithCat = await _dbUnitOfWork.AdoptRepositories.HasAccountRegisterdWithPet(request.AccountId, request.CatId);
        if (hasAccountRegisteredWithCat)
        {
            throw new AdoptApplicationException.AdopterHasAlreadyRegisteredWithCatException();
        }

        var adoptApplication = AdoptPetApplication.CreateAdoptPetApplication(null, 0, false, request.Description, request.AccountId, request.CatId, DateTime.Now, DateTime.Now, false);

        _adoptApplicationRepository.Add(adoptApplication);

        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Create Adopt Pet Application Successfully.");
    }
}
