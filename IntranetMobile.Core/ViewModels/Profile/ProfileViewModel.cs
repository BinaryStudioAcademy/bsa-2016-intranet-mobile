namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel()
        {
            Title = "Profile";
        }

        public MyProfileViewModel MyProfileViewModel { get; private set; }

        public MyExperienceViewModel MyExperienceViewModel { get; private set; }

        public PdpFlowViewModel PdpFlowViewModel { get; private set; }

        public void Init(string userId)
        {
            MyProfileViewModel = new MyProfileViewModel { UserId = userId };
            MyExperienceViewModel = new MyExperienceViewModel { UserId = userId };
            PdpFlowViewModel = new PdpFlowViewModel { UserId = userId };
        }
    }
}