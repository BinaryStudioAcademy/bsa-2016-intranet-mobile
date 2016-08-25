﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UsersViewModel : BaseViewModel
    {
        private UserItemViewModel _selectedItem;
        //private UserViewModel _curretUser;

        public UsersViewModel()
        {
            Title = "Users";
            SelectItem = new MvxCommand<UserItemViewModel>(item => { SelectedItem = item; });
            Task.Run(LoadData);
        }

        public ObservableCollection<UserItemViewModel> Users { set; get; } =
            new ObservableCollection<UserItemViewModel>();

        public UserItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem != null)
                {
                    ShowViewModel<ProfileViewModel>(new {userId = _selectedItem.Id});
                }

                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem { get; private set; }


        public async Task LoadData()
        {
            var users = await ServiceBus.UserService.GetAllUsers();
            var currentUser = ServiceBus.UserService.CurrentUser;
            Users.Add(await UserItemViewModel.FromModel(currentUser));
            foreach (var user in users.Where(user => user.UserId != currentUser.UserId).OrderBy(u => u.FirstName))
            {
                InvokeOnMainThread(async () => { Users.Add(await UserItemViewModel.FromModel(user)); });
            }
        }
    }
}