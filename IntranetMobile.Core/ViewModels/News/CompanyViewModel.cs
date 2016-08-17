using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp.Parser.Html;
using IntranetMobile.Core.Helpers;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CompanyViewModel : BaseNewsViewModel
    {
        private bool _isRefreshing;
        private NewsPreviewViewModel _selectedItem;

        public CompanyViewModel()
        {
            AsyncHelper.RunSync(ReloadData);
        }

        public ObservableCollection<NewsPreviewViewModel> News { set; get; } =
            new ObservableCollection<NewsPreviewViewModel>();

        public NewsPreviewViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                
                ShowViewModel<NewsDetailsViewModel>(new {id = _selectedItem.NewsId});

                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem
        {
            get { return new MvxCommand<NewsPreviewViewModel>(item => { SelectedItem = item; }); }
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

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    IsRefreshing = true;

                    await ReloadData();

                    IsRefreshing = false;
                });
            }
        }

        public virtual async Task ReloadData()
        {
            //TODO: Normalize news pull
            News.Clear();
            var parser = new HtmlParser();
            var news = await ServiceBus.NewsService.CompanyNews(0, 10);
            foreach (var newsDto in news)
            {
                var parseObj = parser.Parse(newsDto.body);
                var image = string.Empty;
                if (parseObj.Images.Length > 0)
                {
                    image = parseObj.Images[0].Source;
                }
                var title = newsDto.title;
                var author = newsDto.authorId;
                News.Add(new NewsPreviewViewModel
                {
                    PreviewImageUri = image,
                    Title = title,
                    Subtitle = author
                });
            }
        }
    }
}