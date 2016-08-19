using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Users
{
    public class UsersViewModel : BaseViewModel
    {
        public UsersViewModel()
        {
            Title = "Users";
            Task.Run(LoadData);
        }

        public ObservableCollection<UserViewModel> Users { set; get; } =
            new ObservableCollection<UserViewModel>();

        public async Task LoadData()
        {
            var users = await ServiceBus.UserService.GetAllUsers();
            foreach (var user in users)
            {
                InvokeOnMainThread(() => { Users.Add(UserViewModel.FromModel(user)); });
            }
        }
    }
}