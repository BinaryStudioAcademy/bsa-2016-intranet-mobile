using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerSectionViewModel : BaseViewModel
    {
        private readonly string _groupId;
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

        public ObservableCollection<ItemReviewViewModel> Reviews { get; set; }
            = new ObservableCollection<ItemReviewViewModel>();

        private bool _isRefreshing;
        public ICommand ReloadCommand { get; private set; }
        public virtual async Task ReloadData()
        {
            try
            {
                var dtos = await ServiceBus.ReviewerService.GetListOfTicketsForConcreteGroupAsync(_groupId);
                InvokeOnMainThread(Reviews.Clear);
                foreach (var dto in dtos)
                {
                    InvokeOnMainThread(() => { Reviews.Add(ItemReviewViewModel.GetItemReviewViewModelFromDto(dto)); });
                }
            }
            catch (Exception e)
            {
               
            }

        }
    }
}
