using System;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserTechnologyViewModel : BaseViewModel
    {
        private int _stars;
        private Technology _technology;

        public int Stars
        {
            get { return _stars; }
            set
            {
                _stars = value;
                RaisePropertyChanged(() => Stars);
            }
        }

        public string TechnologyName => Technology.Name;

        public Technology Technology
        {
            get { return _technology; }
            private set
            {
                _technology = value;
                RaisePropertyChanged(() => Technology);
                RaisePropertyChanged(() => TechnologyName);
            }
        }

        public async void Init(string technologyId, string stars)
        {
            Technology = await ServiceBus.UserService.GetTechnologyById(technologyId);
            Stars = Convert.ToInt32(stars);
        }
    }
}