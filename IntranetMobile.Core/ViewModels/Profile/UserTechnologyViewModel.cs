using System;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserTechnologyViewModel : BaseViewModel
    {
        public UserTechnologyViewModel()
        {
        }

        public UserTechnologyViewModel(string technologyId, int stars)
        {
            Init(technologyId, stars);
        }
        
        public int Stars { get; private set; }

        public string TechnologyName { get; private set; } = "";

        public async void Init(string technologyId, int stars)
        {
            var technology = await ServiceBus.UserService.GetTechnologyById(technologyId);
            if (technology != null)
            {
                TechnologyName = technology.Name;
                Stars = stars;

                RaisePropertyChanged(() => Stars);
                RaisePropertyChanged(() => TechnologyName);
            }
        }
    }
}