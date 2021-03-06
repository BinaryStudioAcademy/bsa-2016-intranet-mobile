﻿using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Login;
using IntranetMobile.Core.ViewModels.News;
using IntranetMobile.Core.ViewModels.Profile;
using IntranetMobile.Core.ViewModels.Reviewer;
using IntranetMobile.Core.ViewModels.Settings;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _avatarUrl;
        private string _email;
        private string _userName;

        public MenuViewModel()
        {
            ShowProfileCommand = new MvxCommand(ShowProfile);
        }

        public MvxCommand ShowProfileCommand { get; }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                RaisePropertyChanged(() => AvatarUrl);
            }
        }

        public void ShowNews()
        {
            ShowViewModel<AllNewsViewModel>();
        }

        public void ShowNewsDetails(string newsId)
        {
            ShowViewModel<NewsDetailsViewModel>(new NewsDetailsViewModel.Parameters { NewsId = newsId });
        }

        public void ShowWeeklyDetails(string weekliesId)
        {
            ShowViewModel<WeekliesDetailsViewModel>(new WeekliesDetailsViewModel.Parameters { WeekliesId = weekliesId });
        }

        public void ShowSettings()
        {
            ShowViewModel<SettingsViewModel>();
        }

        public async void ShowProfile()
        {
            var user = await ServiceBus.UserService.GetCurrentUserAsync();
            ShowViewModel<ProfileViewModel>(new {userId = user.UserId});
        }

        public void ShowReviewer()
        {
            ShowViewModel<ReviewerViewModel>();
        }

        public void ShowReviewerDetails(string requestId)
        {
            ShowViewModel<TicketDetailsViewModel>(new TicketDetailsViewModel.Parameters { TicketId = requestId });
        }

        public void ShowUsers()
        {
            ShowViewModel<UsersViewModel>();
        }

        public void ShowAsciit()
        {
            ShowViewModel<AsciitViewModel>();
        }

        public void Logout()
        {
            ServiceBus.AlertService.ShowDialogBox("Are you sure?",
                "You will be logged out and your stored credentials will be removed",
                "Yes", "No", async () =>
                {
                    var logoutResult = await ServiceBus.AuthService.Logout();
                    if (!logoutResult)
                    {
                        // return;
                        // TODO: Log dat?
                    }
                    var credentials = await ServiceBus.StorageService.GetFirstOrDefault<Credentials>();
                    await ServiceBus.StorageService.RemoveItem(credentials);
                    ShowViewModel<LoginViewModel>();
                });
        }

        public override async void Start()
        {
            base.Start();

            var user = await ServiceBus.UserService.GetCurrentUserAsync();
            UserName = user.FullName;
            Email = user.Email;
            AvatarUrl = Constants.BaseUrl + user.AvatarUri;
        }
    }
}