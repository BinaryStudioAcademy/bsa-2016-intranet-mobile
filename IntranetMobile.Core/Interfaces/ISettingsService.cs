using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface ISettingsService
    {
        Settings GetSettings();

        bool SaveSettings(Settings settings);
    }
}

