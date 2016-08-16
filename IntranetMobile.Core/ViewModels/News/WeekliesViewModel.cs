using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp.Parser.Html;
using IntranetMobile.Core.Helpers;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeekliesViewModel : BaseNewsViewModel
    {
        private NewsPreviewViewModel<NewsDto> _selectedItem;

        public WeekliesViewModel()
        {
            AsyncHelper.RunSync(ReloadData);
        }

        public ObservableCollection<NewsPreviewViewModel<NewsDto>> ListNews { set; get; } = new ObservableCollection<NewsPreviewViewModel<NewsDto>>();

        public NewsPreviewViewModel<NewsDto> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                // TODO: Pass id here
                ShowViewModel<NewsDetailsViewModel>();

                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand SelectItem
        {
            get { return new MvxCommand<NewsPreviewViewModel<NewsDto>>(item => { SelectedItem = item; }); }
        }
        private bool _isRefreshing;

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
            ListNews.Clear();
            var parser = new HtmlParser();
            var weeklies = await ServiceBus.NewsService.Weeklies(0, 10);
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
                    ListNews.Add(new NewsPreviewViewModel<NewsDto>() { ImageUri = image, Title = title, Subtitle = author, Dto = news });
                }              
            }
        }
    }
}
