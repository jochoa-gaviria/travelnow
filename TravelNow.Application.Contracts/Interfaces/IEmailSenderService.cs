
namespace TravelNow.Application.Contracts.Interfaces;

public interface IEmailSenderService
{
    Task<bool> SendEmail(string subject, string body, string email);
}
