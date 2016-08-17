using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp.Parser.Html;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeekliesViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private NewsPreviewViewModel<NewsDto> _selectedItem;

        public WeekliesViewModel()
        {
            Title = "Weeklies";
            Task.Run(ReloadData);
        }

        public ObservableCollection<NewsPreviewViewModel<NewsDto>> ListNews { set; get; } =
            new ObservableCollection<NewsPreviewViewModel<NewsDto>>();

        public NewsPreviewViewModel<NewsDto> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                Task.Run(() => ServiceBus.StorageService.AddItem(_selectedItem.Dto))
                    .ContinueWith(t => ShowViewModel<NewsDetailsViewModel>(new { id = _selectedItem.Dto.Id }));

                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem
        {
            get { return new MvxCommand<NewsPreviewViewModel<NewsDto>>(item => { SelectedItem = item; }); }
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
            var parser = new HtmlParser();
            var weeklies = await ServiceBus.NewsService.Weeklies(0, 10);
            if (ListNews.Count > 0)
                ListNews.Clear();
            
            foreach (var list in weeklies)
            {
                foreach (var news in list.fullNews)
                {
                    var parseObj = parser.Parse(news.body);
                    var image = string.Empty;
                    if (parseObj.Images.Length > 0)
                    {
                        image = parseObj.Images[0].Source;
                    }
                    var title = list.title;
                    var author = list.authorId;
                    ListNews.Add(new NewsPreviewViewModel<NewsDto>
                    {
                        ImageUri = image,
                        Title = title,
                        Subtitle = author,
                        Dto = news
                    });
                }
            }
        }
    }
}