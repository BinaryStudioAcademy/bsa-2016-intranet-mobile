﻿using System.Collections.Generic;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace IntranetMobile.Core.Services
{
    public class ServiceBus
    {
        private static IStorageService _storageService;
        private static IAuthService _authService;
        private static INewsService _newsService;
        private static IAlertService _alertService;
        private static IUserService _userService;
        private static IDeviceInfo _deviceInfo;
        private static ISettingsService _settingsService;
        private static IReviewerService _reviewerService;

        private static IMvxMessenger _messengerHub;

        public static IDeviceInfo DeviceInfo
            => _deviceInfo ?? (_deviceInfo = Mvx.Resolve<IDeviceInfo>());

        public static IStorageService StorageService
            => _storageService ?? (_storageService = Mvx.Resolve<IStorageService>());

        public static IAuthService AuthService
            => _authService ?? (_authService = Mvx.Resolve<IAuthService>());

        public static INewsService NewsService
            => _newsService ?? (_newsService = Mvx.Resolve<INewsService>());

        public static IAlertService AlertService
            => _alertService ?? (_alertService = Mvx.Resolve<IAlertService>());

        public static IUserService UserService
            => _userService ?? (_userService = Mvx.Resolve<IUserService>());

        public static IMvxMessenger MessengerHub
            => _messengerHub ?? (_messengerHub = Mvx.Resolve<IMvxMessenger>());

        public static ISettingsService SettingsService
            => _settingsService ?? (_settingsService = Mvx.Resolve<ISettingsService>());

        public static IReviewerService ReviewerService
        => _reviewerService ?? (_reviewerService = Mvx.Resolve<IReviewerService>());
    }
}