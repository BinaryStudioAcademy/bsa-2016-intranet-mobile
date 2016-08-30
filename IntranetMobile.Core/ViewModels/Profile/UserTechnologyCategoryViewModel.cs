using System.Collections.ObjectModel;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserTechnologyCategoryViewModel : BaseViewModel
    {
        private string _name;

        public ObservableCollection<UserTechnologyViewModel> UserTechnologyViewModels { get; } =
            new ObservableCollection<UserTechnologyViewModel>();

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}