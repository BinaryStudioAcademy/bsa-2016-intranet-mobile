using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerSectionViewModel : BaseViewModel
    {
        public ReviewerSectionViewModel()
        {
            Title = "ReviewerSectionViewModel";

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
                InvokeOnMainThread(() => { Reviews.Add(new ItemReviewViewModel() { TitleName = "Testing Title Name", Author = "Ivan Ivanov", DateTime = DateTime.Now.ToString(), ReviewerText = "This review was created by Ivan Ivanov for testing this code review"}); });
            }
            catch (Exception e)
            {

            }

        }
    }
}
