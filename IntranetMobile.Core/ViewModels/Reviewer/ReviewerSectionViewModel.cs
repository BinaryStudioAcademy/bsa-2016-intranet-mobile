using System;
using System.Collections.Generic;
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

        private readonly List<BaseItemReviewViewModel> _allReviewList = new List<BaseItemReviewViewModel>();
        private bool _isRefreshing;
        private int _vmId = 1;
        private bool _isCurrentUserFilterOn;
        private BaseItemReviewViewModel _selectedItem;

        public ReviewerSectionViewModel()
        {
        }

        public ReviewerSectionViewModel(ReviewerGroup group)
        {
            _group = group;
            SelectItem = new MvxCommand<BaseItemReviewViewModel>(item => { SelectedItem = item; });

            ReloadCommand = new MvxCommand(async () =>
            {
                IsRefreshing = true;
                await ReloadData();
                IsRefreshing = false;
            });

            Task.Run(ReloadData);

            ServiceBus.ReviewerService.ReviewAdded += (obj) =>
            {
                if (obj == _group)
                    Task.Run(ReloadData);
            };

            AddReviewCommand = new MvxCommand(AddReviewCommandExecute);
        }

        public ICommand SelectItem { get; set; }

        public ICommand AddReviewCommand { get; set; }

        public void AddReviewCommandExecute()
        {
            ShowViewModel<NewTicketViewModel>();
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
                UpdateReviewList();
            }
        }

        public ObservableCollection<BaseItemReviewViewModel> Reviews { get; private set; }
            = new ObservableCollection<BaseItemReviewViewModel>();

        public BaseItemReviewViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                    ShowViewModel<TicketDetailsViewModel>(new TicketDetailsViewModel.Parameters { TicketId = _selectedItem.TicketId });
                RaisePropertyChanged(()=> SelectedItem);
            }
        }

        public ICommand ReloadCommand { get; private set; }

        public async Task ReloadData()
        {
            try
            {
                var tickets = await ServiceBus.ReviewerService.GetListOfTicketsForGroupAsync(_group);
                _allReviewList.Clear();
                var user = await ServiceBus.UserService.GetCurrentUserAsync();
                var currentUserId = user.ServerId;
                foreach (var model in tickets)
                {
                    if (model.UserServerId == currentUserId)
                    {
                        var item = ItemUserReviewViewModel.FromModel(model);
                        item.NotifyItemDeleted = ItemDeleted;
                        item.VmId = _vmId;
                        _vmId++;
                        _allReviewList.Add(item);
                    }
                    else
                    {
                        _allReviewList.Add(ItemReviewViewModel.FromModel(model, currentUserId));
                    }
                }

                UpdateReviewList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private void UpdateReviewList()
        {
            var items = _allReviewList.Where(m => !IsCurrentUserFilterOn || m is ItemUserReviewViewModel);

            InvokeOnMainThread(() =>
            {
                Reviews.Clear();
                Reviews = new ObservableCollection<BaseItemReviewViewModel>(items);
                RaisePropertyChanged(() => Reviews);
            });
        }

        private void ItemDeleted(int id)
        {
            var deleted = Reviews.FirstOrDefault(i => i.VmId == id);
            if (deleted != null)
            {
                _allReviewList.Remove(deleted);
                UpdateReviewList();
            }
        }
    }
}