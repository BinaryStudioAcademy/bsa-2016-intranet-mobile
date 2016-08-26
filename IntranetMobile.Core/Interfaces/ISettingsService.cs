using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface ISettingsService
    {
        Task<Settings> GetSettings();

        Task<bool> SaveSettings(Settings settings);
    }
}

