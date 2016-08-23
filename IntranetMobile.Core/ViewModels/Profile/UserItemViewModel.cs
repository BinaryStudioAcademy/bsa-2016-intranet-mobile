using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserItemViewModel : BaseViewModel
    {
        private const string SiteUrl = "http://team.binary-studio.com";
        private string _firstName;
        private string _fullName;
        private string _lastName;
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

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
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

        public static UserItemViewModel FromModel(User user)
        {
            var position = ServiceBus.UserService.GetPositionById(user.PositionId);
            return new UserItemViewModel
            {
                Id = user.UserId,
                PreviewImageUri = SiteUrl + user.AvatarUri,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                PositionName = position != null ? position.Name : "Null"
            };
        }
    }
}