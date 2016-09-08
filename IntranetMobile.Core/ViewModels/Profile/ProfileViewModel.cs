using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _position = string.Empty;
        private bool _stepsVisibility = true;
        private User _user;
        private string _userId;

        public ProfileViewModel()
        {
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
                GetUserData();
            }
        }

        public string FullName => User?.FullName;

        public string Name => User?.FirstName;

        public string Surname => User?.LastName;

        public string Birthday => (User?.Birthday ?? default(DateTime)).ToDateString();

        public string Gender => User?.Gender;

        public string Country => User?.Country;

        public string City => User?.City;

        public string HireDate => (User?.HireDate ?? default(DateTime)).ToDateString();

        public bool TechnologiesVisibility => UserTechnologyCategoryViewModels.Count != 0;

        public bool CertificationsVisibility => Certifications.Count != 0;

        public bool AchievementsVisibility => Achievements.Count != 0;

        public ObservableCollection<BaseViewModel> UserTechnologyCategoryViewModels { get; } =
            new ObservableCollection<BaseViewModel>();

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

        public void Init(string userId)
        {
            UserId = userId;
        }

        private void ChangeStepsVisibilityCommandExecute()
        {
            StepsVisibility = !_stepsVisibility;
        }

        private void GetUserData()
        {
            Task.Run(async () =>
            {
                User = await ServiceBus.UserService.GetUserByServerId(UserId);

                var userCvs = await ServiceBus.UserService.GetUserCvsByServerId(UserId);
                var userInfo = await ServiceBus.UserService.GetUserInfoById(User.UserId);

                Position = userInfo?.Department ?? "?";

                InvokeOnMainThread(() => { UserTechnologyCategoryViewModels.Clear(); });

                var technologyCategoryIds = new Dictionary<string, UserTechnologyCategoryViewModel>();
                foreach (var technologyCvs in userCvs.UserCv.Technologies)
                {
                    if (!technologyCategoryIds.ContainsKey(technologyCvs.Category.Id))
                    {
                        var userTechnologyCategoryViewModel = new UserTechnologyCategoryViewModel
                        {
                            Name = technologyCvs.Category.Name
                        };
                        technologyCategoryIds.Add(technologyCvs.Category.Id, userTechnologyCategoryViewModel);

                        // TODO: Currently replaced with awful approach below
                        //InvokeOnMainThread(
                        //    () => { UserTechnologyCategoryViewModels.Add(userTechnologyCategoryViewModel); });
                    }

                    InvokeOnMainThread(() =>
                    {
                        technologyCategoryIds[technologyCvs.Category.Id].UserTechnologyViewModels.Add(
                            new UserTechnologyViewModel(technologyCvs.Name, technologyCvs.Stars));
                    });
                }

                // TODO: Awful approach suggested due to Nested ListViews are poorly supported.
                // TODO: Anyway, UserTechnologyCategoryViewModel has propriate collection of user technologies to create nested binding in future.
                foreach (var userTechnologyCategoryViewModel in technologyCategoryIds.OrderBy(t => t.Value.Name))
                {
                    InvokeOnMainThread(
                        () =>
                        {
                            UserTechnologyCategoryViewModels.Add(userTechnologyCategoryViewModel.Value);
                            foreach (
                                var userTechnologyViewModel in userTechnologyCategoryViewModel
                                    .Value
                                    .UserTechnologyViewModels
                                    .OrderByDescending(i => i.Stars))
                            {
                                UserTechnologyCategoryViewModels.Add(userTechnologyViewModel);
                            }
                        });
                }

                RaisePropertyChanged(() => TechnologiesVisibility);

                foreach (var achievementId in _user.Pdp.AchievementsIds)
                {
                    var achievement = await ServiceBus.UserService.GetAchievementsById(achievementId);
                    var userAchievementVm = new UserAchievementViewModel
                    {
                        Name = achievement.Name,
                        Category = achievement.Category.Name,
                        ImageUri = Constants.BaseUrl + achievement.ImageUri
                    };
                    InvokeOnMainThread(() => { Achievements.Add(userAchievementVm); });
                }

                RaisePropertyChanged(() => AchievementsVisibility);

                foreach (var certificationId in _user.Pdp.CertificationsIds)
                {
                    var certification = await ServiceBus.UserService.GetCertificateByIdAsync(certificationId);
                    var userCertificationVm = new UserCertificationViewModel
                    {
                        Name = certification.Name,
                        Category = certification.Category.Name,
                        ImageUri = Constants.BaseUrl + certification.ImageUri
                    };
                    InvokeOnMainThread(() => { Certifications.Add(userCertificationVm); });
                }

                RaisePropertyChanged(() => CertificationsVisibility);
            });
        }
    }
}