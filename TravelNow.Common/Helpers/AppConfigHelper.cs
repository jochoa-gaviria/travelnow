using Microsoft.Extensions.Configuration;
using TravelNow.Common.Enums;
using TravelNow.Common.Interfaces;

namespace TravelNow.Common.Helpers;

public class AppConfigHelper : IAppConfigHelper
{
    #region internals
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _connectionStringsSection;
    private readonly IConfigurationSection _smtpConfigurationSection;
    #endregion internals

    #region constructor
    public AppConfigHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionStringsSection = _configuration.GetSection("ConnectionStrings");
        _smtpConfigurationSection = _configuration.GetSection("SmtpConfiguration");
    }
    #endregion constructor

    #region methods
    public Dictionary<EDatabaseSettings, string> DatabaseSettings => new Dictionary<EDatabaseSettings, string>
    {
        { EDatabaseSettings.ConnectionString, _connectionStringsSection.GetSection(nameof(EDatabaseSettings.ConnectionString)).Value },
        { EDatabaseSettings.Database, _connectionStringsSection.GetSection(nameof(EDatabaseSettings.Database)).Value },
    };

    public Dictionary<ESmtpSettings, string> SmtpSettings => new Dictionary<ESmtpSettings, string>
    {
        { ESmtpSettings.Host, _smtpConfigurationSection.GetSection(nameof(ESmtpSettings.Host)).Value },
        { ESmtpSettings.Port, _smtpConfigurationSection.GetSection(nameof(ESmtpSettings.Port)).Value },
        { ESmtpSettings.User, _smtpConfigurationSection.GetSection(nameof(ESmtpSettings.User)).Value },
        { ESmtpSettings.Key, _smtpConfigurationSection.GetSection(nameof(ESmtpSettings.Key)).Value },
        { ESmtpSettings.Sender, _smtpConfigurationSection.GetSection(nameof(ESmtpSettings.Sender)).Value },
    };

    #endregion methods
}
