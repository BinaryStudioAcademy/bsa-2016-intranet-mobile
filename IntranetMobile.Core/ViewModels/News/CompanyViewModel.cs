using System;
using System.Collections.Generic;
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
        private NewsPreviewViewModel<NewsDto> _selectedItem;

        public CompanyViewModel()
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
                ShowViewModel<NewsDetailsViewModel>(SelectedItem);

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
            var news = await ServiceBus.NewsService.CompanyNews(0, 10);
            foreach (var compFullNewsDto in news)
            {
                var parseObj = parser.Parse(compFullNewsDto.body);
                var image = string.Empty;
                if (parseObj.Images.Length > 0)
                {
                    image = parseObj.Images[0].Source;
                }
                var title = compFullNewsDto.title;
                var author = compFullNewsDto.authorId;
                ListNews.Add(new NewsPreviewViewModel<NewsDto>() { ImageUri = image, Title = title, Subtitle = author, Dto = compFullNewsDto});
            }
        }
    }
}