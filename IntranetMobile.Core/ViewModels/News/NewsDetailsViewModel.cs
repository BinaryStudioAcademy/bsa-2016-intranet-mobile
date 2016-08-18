using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private const string Tag = "NewsDetailsViewModel";
        private readonly MvxSubscriptionToken _token;
        private string _title;

        public NewsDetailsViewModel(IMvxMessenger messenger)
        {
            _token = messenger.Subscribe<NewsViewModelMessage>(DeliveryAction);
            LikeCommand = new MvxCommand(Like);
            CommentCommand = new MvxCommand(Comment);
        }

        public override string Title
        {
            get { return _title; }
            protected set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public MvxCommand LikeCommand { get; private set; }

        public MvxCommand CommentCommand { get; private set; }

        public NewsViewModel NewsViewModel { get; private set; }

        private async void DeliveryAction(NewsViewModelMessage newsViewModelMessage)
        {
            NewsViewModel = newsViewModelMessage.ViewModel;
            await NewsViewModel.MetadataReloadAsync();
            Title = NewsViewModel.NewsTitle;
        }

        private void Like()
        {
            ServiceBus.AlertService.ShowMessage(Tag, "Like clicked!");
        }

        private void Comment()
        {
            // TODO: Call CommentsViewModel here
            ServiceBus.AlertService.ShowMessage(Tag, "Comment clicked!");
        }
    }
}