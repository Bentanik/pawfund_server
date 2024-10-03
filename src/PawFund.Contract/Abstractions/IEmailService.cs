﻿namespace PawFund.Contract.Abstractions;

public interface IEmailService
{
    Task<bool> SendMailAsync
        (string toEmail, 
        string subject, 
        string templateName, 
        Dictionary<string, string> Body);
}