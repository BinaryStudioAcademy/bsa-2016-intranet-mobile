using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp.Parser.Html;
using IntranetMobile.Core.Helpers;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CompanyViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private NewsPreviewViewModel<NewsDto> _selectedItem;

        public CompanyViewModel()
        {
            Title = "Company";
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
            var news = await ServiceBus.NewsService.CompanyNews(0, 10);
            if (ListNews.Count > 0)
                ListNews.Clear();
            
            foreach (var newsDto in news)
            {
                var parseObj = parser.Parse(newsDto.body);
                var image = string.Empty;
                if (parseObj.Images.Length > 0)
                {
                    image = parseObj.Images[0].Source;
                }
                var title = newsDto.title;
                var author = ServiceBus.ListUsers.FirstOrDefault(user => user.ServerUserId == newsDto.authorId);
                ListNews.Add(new NewsPreviewViewModel<NewsDto>
                {
                    ImageUri = image,
                    Title = title,
                    Subtitle = $"{author.Name} {author.Surname} on {TimeConvertHelper.ConvertFromUnixTimestamp(newsDto.date)}",
                    Dto = newsDto
                });
            }
        }
    }
}