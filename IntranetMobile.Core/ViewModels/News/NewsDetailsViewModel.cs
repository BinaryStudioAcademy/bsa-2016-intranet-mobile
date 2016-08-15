using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private const string Tag = "NewsDetailsViewModel";
        private bool _isLiked;
        private string _subtitile;
        private string _title;

        public NewsDetailsViewModel()
        {
            LikeCommand = new MvxCommand(Like);
        }

        public MvxCommand LikeCommand { get; private set; }

        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Subtitle
        {
            get { return _subtitile; }
            private set
            {
                _subtitile = value;
                RaisePropertyChanged(() => Subtitle);
            }
        }

        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;
                RaisePropertyChanged(() => IsLiked);
            }
        }

        private void Like()
        {
            IsLiked = !IsLiked;
            ServiceBus.AlertService.ShowMessage(Tag, "Like clicked!");
        }

        public void Init(NewsPreviewViewModel newsPreviewViewModel)
        {
            Title = newsPreviewViewModel.Title;
            Subtitle = newsPreviewViewModel.Subtitle;
            IsLiked = newsPreviewViewModel.IsLiked;
        }
    }
}