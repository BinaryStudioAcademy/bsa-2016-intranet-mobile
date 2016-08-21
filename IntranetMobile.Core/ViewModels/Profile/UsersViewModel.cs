using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UsersViewModel : BaseViewModel
    {
        //private UserViewModel _curretUser;

        public UsersViewModel()
        {
            Title = "Users";
            Task.Run(LoadData);
        }

        public ObservableCollection<UserViewModel> Users { set; get; } =
            new ObservableCollection<UserViewModel>();

        //public UserViewModel CurretUser
        //{
        //    get { return _curretUser; }
        //    set
        //    {
        //        _curretUser = value; 
        //        RaisePropertyChanged(()=> CurretUser);
        //    }
        //}

        public async Task LoadData()
        {
            var users = await ServiceBus.UserService.GetAllUsers();
            var currentUser = ServiceBus.UserService.CurrentUser;
            Users.Add(UserViewModel.FromModel(currentUser));
            foreach (var user in users.Where(user => user.UserId != currentUser.UserId))
            {
                InvokeOnMainThread(() => { Users.Add(UserViewModel.FromModel(user)); });
                await Task.Delay(500);
            }
           
        }
    }
}