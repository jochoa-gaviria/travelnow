using TravelNow.Common.Enums;

namespace TravelNow.Common.Interfaces;

public interface IAppConfigHelper
{
    Dictionary<EDatabaseSettings, string> DatabaseSettings { get; }
    Dictionary<ESmtpSettings, string> SmtpSettings { get; }
}
