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
        private string _position = string.Empty;
        private User _user;
        private string _userId;
        private bool _certificationsVisibility = true;
        private bool _achievementsVisibility = true;
        private bool _stepsVisibility = true;

        public ProfileViewModel()
        {
            ChangeAchievementsVisibilityCommand = new MvxCommand(ChangeAchievementsVisibilityCommandExecute);
            ChangeCertificationsVisibilityCommand = new MvxCommand(ChangeCertificationsVisibilityCommandExecute);
            ChangeStepsVisibilityCommand = new MvxCommand(ChangeStepsVisibilityCommandExecute);
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

        public User User
        {
            get { return _user; }
            private set
            {
                _user = value;

                InvokeOnMainThread(() =>
                {
                    RaisePropertyChanged(() => Name);
                    RaisePropertyChanged(() => Surname);
                    RaisePropertyChanged(() => Birthday);
                    RaisePropertyChanged(() => Gender);
                    RaisePropertyChanged(() => Country);
                    RaisePropertyChanged(() => HireDate);
                    RaisePropertyChanged(() => AvatarUrl);
                });
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
                    User = await ServiceBus.UserService.GetUserById(UserId);
                    Position = (await ServiceBus.UserService.GetPositionById(User.PositionId))?.Name ?? "?";

                    UserTechnologyViewModels.Clear();
                    foreach (var userTechnology in _user.Cv.UserTechnologies.OrderByDescending(t => t.Stars))
                    {
                        // Same check is used insinde UserTechnologyViewModel to eliminate reference passing and do UserTechnologyViewModel more flexible
                        var technology = await ServiceBus.UserService.GetTechnologyById(userTechnology.TechnologyId);
                        if (technology == null)
                        {
                            continue;
                        }
                        var userTechnologyViewModel = new UserTechnologyViewModel(userTechnology.TechnologyId,
                            userTechnology.Stars);
                        UserTechnologyViewModels.Add(userTechnologyViewModel);
                    }


                    foreach (var achievementIds in _user.Pdp.AchievementsIds)
                    {
                        var achievement = await ServiceBus.UserService.GetAchievementsById(achievementIds);
                        var userAchievementVm = new UserAchievementViewModel() {Name = achievement.Name,Category = achievement.Category};
                        Achievements.Add(userAchievementVm);
                    }
                    //foreach (var certificationIds in _user.Pdp.CertificationsIds)
                    //{
                    //    var userCertificationVm = new UserCertificationViewModel() {Name = certification.Name, Category = certification.Category};
                    //    Certifications.Add(userCertificationVm);
                    //}
                    InvokeOnMainThread(() => RaisePropertyChanged(() => TechnologiesVisibility));
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

        public ObservableCollection<UserTechnologyViewModel> UserTechnologyViewModels { get; set; } =
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

        public void Init(string userId)
        {
            UserId = userId;
        }
        public ICommand ChangeAchievementsVisibilityCommand { get;private set; }
        public ICommand ChangeCertificationsVisibilityCommand { get; private set; }
        public ICommand ChangeStepsVisibilityCommand { get; private set; }

        public bool StepsVisibility
        {
            get { return _stepsVisibility; }
            set
            {
                _stepsVisibility = value;
                RaisePropertyChanged(()=> StepsVisibility);
            }
        }

        public bool CertificationsVisibility
        {
            get { return _certificationsVisibility; }
            set
            {
                _certificationsVisibility = value;
                RaisePropertyChanged(()=> CertificationsVisibility);
            }
        }

        public bool AchievementsVisibility
        {
            get { return _achievementsVisibility; }
            set
            {
                _achievementsVisibility = value;
                RaisePropertyChanged(()=> AchievementsVisibility);
            }
        }
    }
}