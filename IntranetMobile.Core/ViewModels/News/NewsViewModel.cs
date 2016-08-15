using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
	public class NewsViewModel : BaseViewModel
    {
        private ItemNewsViewModel selectedItem;

        public ObservableCollection<ItemNewsViewModel> ListNews { set; get; }

        public ItemNewsViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                //TODO: Open News Window
                RaisePropertyChanged(() => SelectedItem);
            }
        }

	    public NewsViewModel()
	    {
            ListNews = new ObservableCollection<ItemNewsViewModel>
            {
                new ItemNewsViewModel {Title = "New1", SubTitle = "Author111"},
                new ItemNewsViewModel {Title = "New2", SubTitle = "Author222"},
                new ItemNewsViewModel {Title = "New3", SubTitle = "Author333"}
            };
	    }

        public ICommand SelectItem
        {
            get { return new MvxCommand<ItemNewsViewModel>(item => { SelectedItem = item; }); }
        }
    }
}