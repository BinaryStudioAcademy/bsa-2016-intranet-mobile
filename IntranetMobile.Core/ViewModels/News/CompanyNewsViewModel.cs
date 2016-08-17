﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp.Parser.Html;
using IntranetMobile.Core.Helpers;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CompanyNewsViewModel : BaseNewsViewModel
    {
        private bool _isRefreshing;
        private NewsViewModel _selectedItem;
        private readonly MvxSubscriptionToken _token;

        public CompanyNewsViewModel(IMvxMessenger messenger)
        {
            _token = messenger.Subscribe<MvxSubscriberChangeMessage>(MvxSubscriberChangeMessageDeliveryAction);
            AsyncHelper.RunSync(ReloadData);
        }

        private void MvxSubscriberChangeMessageDeliveryAction(MvxSubscriberChangeMessage subscriberChangeMessage)
        {
            if (subscriberChangeMessage.MessageType == typeof(NewsViewModelMessage))
            {
                var message = new NewsViewModelMessage(this, _selectedItem);
                ServiceBus.MessengerHub.Publish(message);
            }
        }

        public ObservableCollection<NewsViewModel> News { set; get; } =
            new ObservableCollection<NewsViewModel>();

        public NewsViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ShowViewModel<NewsDetailsViewModel>();
                
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
            var allNews = await ServiceBus.NewsService.CompanyNews(0, 10);
            foreach (var news in allNews)
            {
                var parseObj = parser.Parse(news.body);
                var image = string.Empty;
                if (parseObj.Images.Length > 0)
                {
                    image = parseObj.Images[0].Source;
                }

                News.Add(new NewsViewModel
                {
                    PreviewImageUri = image,
                    NewsId = news.newsId
                });
            }
        }
    }
}