﻿using Newtonsoft.Json;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.DTOs.AuthenticationDTOs;
using System.Net.Http.Headers;

namespace PawFund.Infrastructure.Services;

public class GoogleOAuthService : IGoogleOAuthService
{
    private readonly HttpClient _httpClient;

    public GoogleOAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GoogleUserInfoDTO> ValidateTokenAsync(string AccessTokenGoogle)
    {
        var request = new HttpRequestMessage
            (HttpMethod.Get,
            "https://www.googleapis.com/oauth2/v3/userinfo");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessTokenGoogle);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var userInfo = JsonConvert.DeserializeObject<GoogleUserInfoDTO>(content);

        return userInfo;
    }
}
