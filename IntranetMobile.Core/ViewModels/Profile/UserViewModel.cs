using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserViewModel : BaseViewModel
    {
        private string _firstName;
        private string _fullName;
        private string _lastName;
        private string _position;
        private string _previewImageUri;
        private const string SiteUrl = "http://team.binary-studio.com";
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

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
            }
        }

        public string Id { get; set; }

        public static UserViewModel FromModel(User user)
        {
            return new UserViewModel
            {
                Id = user.UserId,
                PreviewImageUri = SiteUrl + user.AvatarUri,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                Position = user.Position != null ? user.Position.Name : "Null"
            };
        }
    }
}