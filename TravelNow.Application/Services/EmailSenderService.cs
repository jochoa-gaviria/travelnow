using System.Net.Mail;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Common.Interfaces;
using TravelNow.Common.Enums;

namespace TravelNow.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    #region internals
    private readonly IAppConfigHelper _appConfig;
    #endregion internals

    #region constructor
    public EmailSenderService(IAppConfigHelper appConfig)
    {
        _appConfig = appConfig;
    }
    #endregion constructor

    #region methods

    public async Task<bool> SendEmail(string subject, string body, string email)
    {

        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_appConfig.SmtpSettings[ESmtpSettings.Sender]);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = false;

            Int32.TryParse(_appConfig.SmtpSettings[ESmtpSettings.Port], out Int32 port);

            SmtpClient smtpClient = new SmtpClient(_appConfig.SmtpSettings[ESmtpSettings.Host], port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_appConfig.SmtpSettings[ESmtpSettings.User], _appConfig.SmtpSettings[ESmtpSettings.Key]);
            smtpClient.EnableSsl = false;

            // Enviar el correo electrónico
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception)
        {
            return false;
        }
        return true;

    }

    #endregion methods
}
