using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerViewModel : BaseViewModel
    {
        public ReviewerViewModel()
        {
            Title = "Reviewer";
           
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
        public ObservableCollection<ItemReviewViewModel> Reviews 
            = new ObservableCollection<ItemReviewViewModel>();

        private bool _isRefreshing;
        public ICommand ReloadCommand { get; private set; }
        public virtual async Task ReloadData()
        {
            try
            {         
               InvokeOnMainThread(() => { Reviews.Add(new ItemReviewViewModel() { TitleName = "123", Author = "daun", DateTime = "12354" }); });               
            }
            catch (Exception e)
            {

            }

        }
    }
}
