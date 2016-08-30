using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private bool _achievementsVisibility = true;
        private bool _certificationsVisibility = true;
        private string _position = string.Empty;
        private bool _stepsVisibility = true;
        private User _user;
        private string _userId;

        public ProfileViewModel()
        {
            ChangeAchievementsVisibilityCommand = new MvxCommand(ChangeAchievementsVisibilityCommandExecute);
            ChangeCertificationsVisibilityCommand = new MvxCommand(ChangeCertificationsVisibilityCommandExecute);
            ChangeStepsVisibilityCommand = new MvxCommand(ChangeStepsVisibilityCommandExecute);
        }

        public User User
        {
            get { return _user; }
            private set
            {
                _user = value;

                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => Surname);
                RaisePropertyChanged(() => FullName);
                RaisePropertyChanged(() => Birthday);
                RaisePropertyChanged(() => Gender);
                RaisePropertyChanged(() => Country);
                RaisePropertyChanged(() => HireDate);
                RaisePropertyChanged(() => AvatarUrl);
            }
        }

        public string UserId
        {
            get { return _userId; }
            private set
            {
                _userId = value;
                Task.Run(async () =>
                {
                    User = await ServiceBus.UserService.GetUserByServerId(UserId);
                    
                    var userCvs = await ServiceBus.UserService.GetUserCvsByServerId(UserId);
                    var userInfo = await ServiceBus.UserService.GetUserInfoById(User.UserId);

                    Position = userInfo?.Department ?? "?";

                    InvokeOnMainThread(() => { UserTechnologyViewModels.Clear(); });
                    foreach (var technologyCvs in userCvs.UserCv.Technologies.OrderByDescending(t => t.Stars))
                    {
                        var userTechnologyViewModel = new UserTechnologyViewModel(technologyCvs.Name,
                            technologyCvs.Stars);
                        InvokeOnMainThread(() => { UserTechnologyViewModels.Add(userTechnologyViewModel); });
                    }

                    foreach (var achievement in _user.Pdp.Achievements)
                    {
                        var userAchievementVm = new UserAchievementViewModel
                        {
                            Name = achievement.Name,
                            Category = achievement.Category
                        };
                        InvokeOnMainThread(() => { Achievements.Add(userAchievementVm); });
                    }

                    foreach (var certification in _user.Pdp.Certifications)
                    {
                        var userCertificationVm = new UserCertificationViewModel
                        {
                            Name = certification.Name,
                            Category = certification.Category
                        };
                        InvokeOnMainThread(() => { Certifications.Add(userCertificationVm); });
                    }

                    RaisePropertyChanged(() => TechnologiesVisibility);
                });
            }
        }

        public string FullName => User?.FullName;

        public string Name => User?.FirstName;

        public string Surname => User?.LastName;

        public string Birthday => (User?.Birthday ?? default(DateTime)).ToString("dd MMMM yyyy");

        public string Gender => User?.Gender;

        public string Country => User?.Country;

        public string City => User?.City;

        public string HireDate => (User?.HireDate ?? default(DateTime)).ToString("dd MM yyyy");

        public bool TechnologiesVisibility => UserTechnologyViewModels.Count != 0;

        public ObservableCollection<UserTechnologyViewModel> UserTechnologyViewModels { get; } =
            new ObservableCollection<UserTechnologyViewModel>();

        public ObservableCollection<UserAchievementViewModel> Achievements { get; set; } =
            new ObservableCollection<UserAchievementViewModel>();

        public ObservableCollection<UserCertificationViewModel> Certifications { get; set; } =
            new ObservableCollection<UserCertificationViewModel>();

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
            }
        }

        public string AvatarUrl => User != null
            ? Constants.BaseUrl + User.AvatarUri
            : null;

        public ICommand ChangeAchievementsVisibilityCommand { get; private set; }
        public ICommand ChangeCertificationsVisibilityCommand { get; private set; }
        public ICommand ChangeStepsVisibilityCommand { get; private set; }

        public bool StepsVisibility
        {
            get { return _stepsVisibility; }
            set
            {
                _stepsVisibility = value;
                RaisePropertyChanged(() => StepsVisibility);
            }
        }

        public bool CertificationsVisibility
        {
            get { return _certificationsVisibility; }
            set
            {
                _certificationsVisibility = value;
                RaisePropertyChanged(() => CertificationsVisibility);
            }
        }

        public bool AchievementsVisibility
        {
            get { return _achievementsVisibility; }
            set
            {
                _achievementsVisibility = value;
                RaisePropertyChanged(() => AchievementsVisibility);
            }
        }

        private void ChangeStepsVisibilityCommandExecute()
        {
            StepsVisibility = !_stepsVisibility;
        }

        private void ChangeCertificationsVisibilityCommandExecute()
        {
            CertificationsVisibility = !_certificationsVisibility;
        }

        private void ChangeAchievementsVisibilityCommandExecute()
        {
            AchievementsVisibility = !_achievementsVisibility;
        }

        public void Init(string userId)
        {
            UserId = userId;
        }
    }
}