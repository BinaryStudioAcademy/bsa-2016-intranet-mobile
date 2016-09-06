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
        private readonly string _groupId;
        private int _vmId = 1;

        private bool _isRefreshing;

        public ReviewerSectionViewModel(string groupId)
        {
            Title = "ReviewerSectionViewModel";
            _groupId = groupId;
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
        private  void ItemDeleted(int id)
        {
            foreach (var baseItemReviewViewModel in Reviews)
            {
                if (baseItemReviewViewModel.VmId == id)
                {
                    Reviews.Remove(baseItemReviewViewModel);
                    return;
                }
            }
        }
        public virtual async Task ReloadData()
        {
            try
            {
                var dtos = await ServiceBus.ReviewerService.GetListOfTicketsForConcreteGroupAsync(_groupId);
                InvokeOnMainThread(Reviews.Clear);
                var userId = ServiceBus.UserService.CurrentUser.ServerId;
                foreach (var dto in dtos)
                {
                    if (dto.user.binary_id == userId)
                    {
                        InvokeOnMainThread(
                            () =>
                            {
                                var item = ItemUserReviewViewModel.GetItemReviewViewModelFromDto(dto);
                                item.NotifyItemDeleted = ItemDeleted;
                                item.VmId = _vmId;
                                _vmId++;
                                Reviews.Add(item);
                            });
                    }
                    else
                    {
                        InvokeOnMainThread(
                            () =>
                            {
                                Reviews.Add(ItemReviewViewModel.GetItemReviewViewModelFromDto(dto, userId));
                            });
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}