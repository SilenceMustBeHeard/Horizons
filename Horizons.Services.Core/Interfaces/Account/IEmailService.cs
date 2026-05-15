using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Core.Interfaces.Account;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}