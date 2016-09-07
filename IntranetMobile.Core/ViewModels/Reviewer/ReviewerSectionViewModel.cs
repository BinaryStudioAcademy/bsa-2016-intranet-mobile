using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private int _vmId = 1;
        private bool _isCurrentUserFilterOn;

        public ReviewerSectionViewModel()
        {
        }

        public ReviewerSectionViewModel(ReviewerGroup group)
        {
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

        public bool IsCurrentUserFilterOn
        {
            get { return _isCurrentUserFilterOn; }
            set
            {
                _isCurrentUserFilterOn = value;
                Task.Run(ReloadData);
            }
        }

        public ObservableCollection<BaseItemReviewViewModel> Reviews { get; }
            = new ObservableCollection<BaseItemReviewViewModel>();

        public ICommand ReloadCommand { get; private set; }

        public async Task ReloadData()
        {
            try
            {
                var tickets = await ServiceBus.ReviewerService.GetListOfTicketsForGroupAsync(_group);
                InvokeOnMainThread(Reviews.Clear);
                var currentUserId = ServiceBus.UserService.CurrentUser.ServerId;
                foreach (var model in tickets)
                {
                    if (model.UserServerId == currentUserId)
                    {
                        InvokeOnMainThread(() =>
                        {
                            var item = ItemUserReviewViewModel.FromModel(model);
                            item.NotifyItemDeleted = ItemDeleted;
                            item.VmId = _vmId;
                            _vmId++;
                            Reviews.Add(item);
                        });
                    }
                    else
                    {
                        if(!IsCurrentUserFilterOn)
                        InvokeOnMainThread(() =>
                        {
                            Reviews.Add(ItemReviewViewModel.FromModel(model, currentUserId));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private void ItemDeleted(int id)
        {
            var deleted = Reviews.FirstOrDefault(i => i.VmId == id);
            if (deleted != null) 
                Reviews.Remove(deleted);
        }
    }
}