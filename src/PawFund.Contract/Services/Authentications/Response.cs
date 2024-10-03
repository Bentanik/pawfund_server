﻿namespace PawFund.Contract.Services.Authentications;

public static class Response
{
    public record LoginResponse
        (Guid Id,
        string FirstName,
        string LastName,
        string AccessToken,
        string RefreshToken);

    public record RefreshTokenResponse
        (string AccessToken,
        string RefreshToken);
}