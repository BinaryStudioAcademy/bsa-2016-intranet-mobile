using System;
using System.Threading;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IStorageService _storageService;

        public SettingsService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public Task<Settings> GetSettings()
        {
            return _storageService.GetFirstOrDefault<Settings>();
        }

        public Task<bool> SaveSettings(Settings settings)
        {
            return _storageService.AddOrUpdateItem(settings);
        }
    }
}

