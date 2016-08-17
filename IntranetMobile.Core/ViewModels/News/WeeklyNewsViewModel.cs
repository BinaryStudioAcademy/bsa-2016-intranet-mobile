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
    public class WeeklyNewsViewModel : BaseNewsViewModel
    {
        private bool _isRefreshing;
        private NewsViewModel _selectedItem;

        public WeeklyNewsViewModel()
        {
            AsyncHelper.RunSync(ReloadData);
        }

        public ObservableCollection<NewsViewModel> News { set; get; } =
            new ObservableCollection<NewsViewModel>();

        public NewsViewModel SelectedItem
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
            get { return new MvxCommand<NewsViewModel>(item => { SelectedItem = item; }); }
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
            var weeklies = await ServiceBus.NewsService.Weeklies(0, 10);
            foreach (var newsBundle in weeklies)
            {
                foreach (var news in newsBundle.fullNews)
                {
                    var parseObj = parser.Parse(news.body);
                    var previewImageUrl = string.Empty;
                    if (parseObj.Images.Length > 0)
                    {
                        previewImageUrl = parseObj.Images[0].Source;
                    }

                    News.Add(new NewsViewModel
                    {
                        PreviewImageUri = previewImageUrl,
                        NewsId = news.newsId
                    });
                }
            }
        }
    }
}