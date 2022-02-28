using Departure_Management.Application.Models;

namespace Departure_Management.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}