﻿using Newtonsoft.Json;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using static PawFund.Domain.Exceptions.AuthenticationException;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Authentication;

public sealed class FogotPasswordOtpCommandHandler : ICommandHandler<Command.ForgotPasswordOtpCommand>
{
    private readonly IResponseCacheService _responseCacheService;

    public FogotPasswordOtpCommandHandler(IResponseCacheService responseCacheService)
    {
        _responseCacheService = responseCacheService;
    }
    /// <summary>
    /// Verify otp to continue password change step
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="OtpForgotPasswordNotMatchException"></exception>
    public async Task<Result> Handle(Command.ForgotPasswordOtpCommand request, CancellationToken cancellationToken)
    {
        // Get value from memory
        var forgotPasswordMemory = await _responseCacheService.GetCacheResponseAsync($"forgotpassword_{request.Email}");
        string unescapedJson = JsonConvert.DeserializeObject<string>(forgotPasswordMemory);
        var otp = JsonConvert.DeserializeObject<string>(unescapedJson);

        // Check if the otp created from the previous step matches the otp sent by the client
        if (request.Otp != otp) throw new OtpForgotPasswordNotMatchException();

        // Delete forgot password memory
        await _responseCacheService.DeleteCacheResponseAsync($"forgotpassword_{request.Email}");
        // Save memory
        await _responseCacheService.SetCacheResponseAsync
                ($"passwordchange_{request.Email}",
                JsonConvert.SerializeObject(otp),
                TimeSpan.FromMinutes(15));

        return Result.Success(new Success<string>(MessagesList.AuthForgotPasswordOtpSuccess.GetMessage().Code,
            MessagesList.AuthForgotPasswordOtpSuccess.GetMessage().Message, otp));
    }
}
