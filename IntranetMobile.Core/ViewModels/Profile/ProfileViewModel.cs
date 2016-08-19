using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private User _dataModel;

        public ProfileViewModel()
        {
            Title = "Profile";
        }
    }
}