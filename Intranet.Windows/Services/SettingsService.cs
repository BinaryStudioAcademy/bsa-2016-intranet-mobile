using System;
using Windows.Storage;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;

namespace Intranet.WindowsUWP.Services
{
    public class SettingsService : ISettingsService
    {
        private const string AppSettingsKey = "AppSettings";
        private const string IsVibrationEnabledKey = "IsVibrationEnabled";
        private const string IsNewsNotificationEnabledKey = "IsNewsNotificationEnabled";
        private const string IsReviewerNotificationEnabledKey = "IsReviewerNotificationEnabled";

        public Settings GetSettings()
        {
            var settings = new Settings();
            var localSettings = ApplicationData.Current.LocalSettings;
            var composite = (ApplicationDataCompositeValue)localSettings.Values[AppSettingsKey];

            settings.IsVibrationEnabled = (bool)composite[IsVibrationEnabledKey];
            settings.IsNewsNotificationEnabled = (bool)composite[IsNewsNotificationEnabledKey];
            settings.IsReviewerNotificationEnabled = (bool)composite[IsReviewerNotificationEnabledKey];

            return settings;
        }

        public bool SaveSettings(Settings settings)
        {
            try
            {
                var composite = new ApplicationDataCompositeValue();
                composite[IsVibrationEnabledKey] = settings.IsVibrationEnabled;
                composite[IsNewsNotificationEnabledKey] = settings.IsNewsNotificationEnabled;
                composite[IsReviewerNotificationEnabledKey] = settings.IsReviewerNotificationEnabled;

                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values[AppSettingsKey] = composite;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
