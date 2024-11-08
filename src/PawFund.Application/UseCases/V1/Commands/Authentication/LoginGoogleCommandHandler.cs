﻿using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Contract.Enumarations.Authentication;
using static PawFund.Domain.Exceptions.AuthenticationException;

namespace PawFund.Application.UseCases.V1.Commands.Authentication;

public sealed class LoginGoogleCommandHandler : ICommandHandler<Command.LoginGoogleCommand, Response.LoginResponse>
{
    private readonly IGoogleOAuthService _googleOAuthService;
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IPublisher _publisher;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public LoginGoogleCommandHandler
        (IGoogleOAuthService googleOAuthService,
        IDPUnitOfWork dPUnitOfWork,
        IEFUnitOfWork efUnitOfWork,
        IPublisher publisher,
        ITokenGeneratorService tokenGeneratorService)
    {
        _googleOAuthService = googleOAuthService;
        _dpUnitOfWork = dPUnitOfWork;
        _efUnitOfWork = efUnitOfWork;
        _publisher = publisher;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<Result<Response.LoginResponse>> Handle(Command.LoginGoogleCommand request, CancellationToken cancellationToken)
    {
        // Get info user from access token Google
        var googleUserInfo = await _googleOAuthService.ValidateTokenAsync(request.AccessTokenGoogle);
        // If return == null => Exception
        if (googleUserInfo == null) throw new LoginGoogleFailException();
        // Check email have exit
        var account = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(googleUserInfo.Email);
        // If have not account => Register account with type login Google
        if (account == null)
        {
            // Create object account member
            var accountMember = Domain.Entities.Account.CreateMemberAccountGoogle
                (googleUserInfo.Name, googleUserInfo.Name, googleUserInfo.Email, GenderType.Male);
            _efUnitOfWork.AccountRepository.Add(accountMember);
            // Save account
            await _efUnitOfWork.SaveChangesAsync();
            // Send mail when created success
            await Task.WhenAll(
                _publisher.Publish(new DomainEvent.UserCreatedWithGoogle(Guid.NewGuid(), googleUserInfo.Email),
                cancellationToken)
            );

            // Generate accessToken and refreshToken
            var accessToken = _tokenGeneratorService.GenerateAccessToken(accountMember.Id, (int)accountMember.RoleId);
            var refrehsToken = _tokenGeneratorService.GenerateRefreshToken(accountMember.Id, (int)accountMember.RoleId);

            return Result.Success
                (new Response.LoginResponse
                (accountMember.Id,
                accountMember.FirstName,
                accountMember.LastName,
                accountMember.CropAvatarUrl,
                accountMember.FullAvatarUrl,
                (int)accountMember.RoleId,
                accessToken,
                refrehsToken));
        }
        else
        {
            // If have account, check account not type Google
            if (account.LoginType != LoginType.Google) throw new AccountRegisteredAnotherMethodException();

            // If account banned
            if (account.IsDeleted == true) throw new AccountBanned();

            // Generate accessToken and refreshToken
            var accessToken = _tokenGeneratorService.GenerateAccessToken(account.Id, (int)account.RoleId);
            var refrehsToken = _tokenGeneratorService.GenerateRefreshToken(account.Id, (int)account.RoleId);

            return Result.Success
                (new Response.LoginResponse
                (account.Id,
                account.FirstName,
                account.LastName,
                account.CropAvatarUrl,
                account.FullAvatarUrl,
                (int)account.RoleId,
                accessToken,
                refrehsToken));
        }
    }
}