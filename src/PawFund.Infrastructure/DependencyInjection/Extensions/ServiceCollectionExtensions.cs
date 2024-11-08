﻿using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Settings;
using PawFund.Infrastructure.Services;
using PawFund.Infrastructure.Worker;
using StackExchange.Redis;

namespace PawFund.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddConfigurationRedis
        (this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = new RedisSetting();
        configuration.GetSection(RedisSetting.SectionName).Bind(redisSettings);
        services.AddSingleton<RedisSetting>();
        if (!redisSettings.Enabled) return;
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisSettings.ConnectionString));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings.ConnectionString;
        });
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
    }
   
    public static void AddConfigurationService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddTransient<IPasswordHashService, PasswordHashService>();
        services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
        services.AddTransient<IGoogleOAuthService, GoogleOAuthService>();
        services.AddTransient<IMediaService, MediaService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IDialogflowService, DialogflowService>();
    }

    public static void AddConfigurationAppSetting
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticationSetting>(configuration.GetSection(AuthenticationSetting.SectionName));
        services.Configure<EmailSetting>(configuration.GetSection(EmailSetting.SectionName));
        services.Configure<CloudinarySetting>(configuration.GetSection(CloudinarySetting.SectionName));
        services.Configure<PayOSSetting>(configuration.GetSection(PayOSSetting.SectionName));
        services.Configure<ClientSetting>(configuration.GetSection(ClientSetting.SectionName));
        services.Configure<DialogflowSetting>(configuration.GetSection(DialogflowSetting.SectionName));
        services.Configure<AccountStaffAssistantSetting>(configuration.GetSection(AccountStaffAssistantSetting.SectionName));
        services.Configure<AccountStaffBotSetting>(configuration.GetSection(AccountStaffBotSetting.SectionName));
    }

    public static void AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Cấu hình Hangfire sử dụng SQL Server hoặc một backend lưu trữ khác
        services.AddHangfire(config =>
            config.UseSqlServerStorage(configuration.GetConnectionString("ConnectionStrings")));

        services.AddHangfireServer();

        // Đăng ký dịch vụ IBackgroundJobService
        services.AddTransient<IBackgroundJobService, HangfireBackgroundJobService>();
    }
}
