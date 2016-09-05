using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerSectionViewModel : BaseViewModel
    {
        private readonly ReviewerGroup _group;

        private bool _isRefreshing;

        public ReviewerSectionViewModel(ReviewerGroup group)
        {
            Title = "ReviewerSectionViewModel";
            _group = group;
            ReloadCommand = new MvxCommand(async () =>
            {
                IsRefreshing = true;
                await ReloadData();
                IsRefreshing = false;
            });

            Task.Run(ReloadData);
        }

        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public ObservableCollection<BaseItemReviewViewModel> Reviews { get; set; }
            = new ObservableCollection<BaseItemReviewViewModel>();

        public ICommand ReloadCommand { get; private set; }

        public virtual async Task ReloadData()
        {
            try
            {
                var dtos = await ServiceBus.ReviewerService.GetListOfTicketsForGroupAsync(_group);
                InvokeOnMainThread(Reviews.Clear);
                var userId = ServiceBus.UserService.CurrentUser.ServerId;
                foreach (var dto in dtos)
                {
                    if (dto.user.binary_id == userId)
                    {
                        InvokeOnMainThread(
                            () => { Reviews.Add(ItemUserReviewViewModel.GetItemReviewViewModelFromDto(dto)); });
                    }
                    else
                    {
                        InvokeOnMainThread(
                            () => { Reviews.Add(ItemReviewViewModel.GetItemReviewViewModelFromDto(dto, userId)); });
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}