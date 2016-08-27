using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private Models.Settings _appSettings;

        public SettingsViewModel()
        {
            _appSettings = new Models.Settings();
            Title = "Settings";
            _appSettings = ServiceBus.SettingsService.GetSettings();
        }

        public bool IsVibrationEnabled
        {
            get
            {
                return _appSettings.IsVibrationEnabled;
            }
            set
            {
                _appSettings.IsVibrationEnabled = value;
                SaveSettings();
                RaisePropertyChanged(() => IsVibrationEnabled);
            }
        }

        public bool IsNewsNotificationEnabled
        {
            get
            {
                return _appSettings.IsNewsNotificationEnabled;
            }
            set
            {
                _appSettings.IsNewsNotificationEnabled = value;
                SaveSettings();
                RaisePropertyChanged(() => IsNewsNotificationEnabled);
            }
        }

        public bool IsReviewerNotificationEnabled
        {
            get
            {
                return _appSettings.IsReviewerNotificationEnabled;
            }
            set
            {
                _appSettings.IsReviewerNotificationEnabled = value;
                SaveSettings();
                RaisePropertyChanged(() => IsReviewerNotificationEnabled);
            }
        }

        private void SaveSettings()
        {
            ServiceBus.SettingsService.SaveSettings(_appSettings);
        }
    }
}

