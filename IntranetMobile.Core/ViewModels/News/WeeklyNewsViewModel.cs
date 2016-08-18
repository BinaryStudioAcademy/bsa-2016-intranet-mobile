using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp.Parser.Html;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeeklyNewsViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private NewsViewModel _selectedItem;

        public WeeklyNewsViewModel()
        {
            Title = "Weeklies";
            Task.Run(ReloadData);
        }

        public ObservableCollection<NewsViewModel> News { set; get; } =
            new ObservableCollection<NewsViewModel>();

        public NewsViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                // TODO: Logics here

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
            //News.Clear(); // Zero size check is already inlined

            //var parser = new HtmlParser();
            //var weeklies = await ServiceBus.NewsService.GetWeeklyNews(0, 10);

            //foreach (var newsBundle in weeklies)
            //{
            //    foreach (var news in newsBundle.fullNews)
            //    {
            //        var parseObj = parser.Parse(news.body);
            //        var previewImageUrl = string.Empty;
            //        if (parseObj.Images.Length > 0)
            //        {
            //            previewImageUrl = parseObj.Images[0].Source;
            //        }

            //        News.Add(new NewsViewModel
            //        {
            //            PreviewImageUri = previewImageUrl,
            //            NewsId = news.newsId
            //        });
            //    }
            //}
        }
    }
}