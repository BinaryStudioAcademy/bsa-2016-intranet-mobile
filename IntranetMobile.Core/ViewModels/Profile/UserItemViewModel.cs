using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserItemViewModel : BaseViewModel
    {
        private string _fullName;
        private string _positionName;
        private string _previewImageUri;

        public string PreviewImageUri
        {
            get { return _previewImageUri; }
            set
            {
                _previewImageUri = value;
                RaisePropertyChanged(() => PreviewImageUri);
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        public string PositionName
        {
            get { return _positionName; }
            set
            {
                _positionName = value;
                RaisePropertyChanged(() => PositionName);
            }
        }

        public string Id { get; set; }

        public static UserItemViewModel FromModel(UserInfo user)
        {
            return new UserItemViewModel
            {
                Id = user.UserId,
                PreviewImageUri = Constants.BaseUrl + user.AvatarUri,
                FullName = user.FullName,
                PositionName = user.Department
            };
        }
    }
}