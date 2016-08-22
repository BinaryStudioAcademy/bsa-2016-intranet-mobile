namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel()
        {
            Title = "Profile";
        }

        public void Init(string userId)
        {
            ShowViewModel<MyProfileViewModel>(new {userId});
        }
    }
}