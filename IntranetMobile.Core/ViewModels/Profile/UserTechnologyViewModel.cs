namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserTechnologyViewModel : BaseViewModel
    {
        private int _stars;
        private string _technologyName;

        public UserTechnologyViewModel(string technologyName, int stars)
        {
            Init(technologyName, stars);
        }

        public int Stars
        {
            get { return _stars; }
            private set
            {
                _stars = value;
                RaisePropertyChanged(() => Stars);
            }
        }

        public string TechnologyName
        {
            get { return _technologyName; }
            private set
            {
                _technologyName = value;
                RaisePropertyChanged(() => TechnologyName);
            }
        }

        public void Init(string technologyName, int stars)
        {
            TechnologyName = technologyName;
            Stars = stars;
        }
    }
}