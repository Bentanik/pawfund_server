﻿using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;

namespace PawFund.Application.UseCases.V1.Events;

public class SendEmailWhenUserChangedEventHandler 
    : IDomainEventHandler<DomainEvent.UserCreated>, 
    IDomainEventHandler<DomainEvent.UserVerifiedEmailRegist>,
    IDomainEventHandler<DomainEvent.UserOtpChanged>,
    IDomainEventHandler<DomainEvent.UserPasswordChanged>,
    IDomainEventHandler<DomainEvent.UserCreatedWithGoogle>

{
    private readonly IEmailService _emailService;

    public SendEmailWhenUserChangedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(DomainEvent.UserCreated notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
            (notification.Email,
            "Register PawFund",
            "EmailRegister.html", new Dictionary<string, string> {
            { "ToEmail", notification.Email},
            {"Link", $"https://www.facebook.com"}
        });
    }

    public async Task Handle(DomainEvent.UserVerifiedEmailRegist notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
           (notification.Email,
           "VerifyEmail PawFund",
           "EmailRegister.html", new Dictionary<string, string> {
            { "ToEmail", notification.Email},
            {"Link", $"https://www.facebook.com"}
       });
    }

    public async Task Handle(DomainEvent.UserOtpChanged notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
            (notification.Email,
            "Forgot password PawFund",
            "EmailForgotPassword.html", new Dictionary<string, string> {
            {"ToEmail", notification.Email},
            {"Otp", notification.Otp}
        });
    }

    public async Task Handle(DomainEvent.UserPasswordChanged notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
            (notification.Email,
            "Password changed",
            "EmailPasswordChanged.html", new Dictionary<string, string> {
            {"ToEmail", notification.Email},
        });
    }

    public async Task Handle(DomainEvent.UserCreatedWithGoogle notification, CancellationToken cancellationToken)
    {
        await _emailService.SendMailAsync
            (notification.Email,
            "Register with Google",
            "EmailRegister.html", new Dictionary<string, string> {
            {"ToEmail", notification.Email},
        });
    }
}
