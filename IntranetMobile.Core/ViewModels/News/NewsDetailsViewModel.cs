using System.Linq;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private const string Tag = "NewsDetailsViewModel";
        private int _commentsCount;
        private string _content;
        private bool _isLiked;
        private int _likesCount;

        private NewsDto _news;
        private string _subtitile;
        private string _title;

        public NewsDetailsViewModel()
        {
            LikeCommand = new MvxCommand(Like);
            CommentCommand = new MvxCommand(Comment);
        }

        public MvxCommand LikeCommand { get; private set; }

        public MvxCommand CommentCommand { get; private set; }

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
            private set
            {
                _isLiked = value;
                RaisePropertyChanged(() => IsLiked);
            }
        }

        public int LikesCount
        {
            get { return _likesCount; }
            private set
            {
                _likesCount = value;
                RaisePropertyChanged(() => LikesCount);
            }
        }

        public int CommentsCount
        {
            get { return _commentsCount; }
            set
            {
                _commentsCount = value;
                RaisePropertyChanged(() => CommentsCount);
            }
        }

        public string Content
        {
            get { return _content; }
            private set
            {
                _content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        private void Like()
        {
            IsLiked = !IsLiked;
            ServiceBus.AlertService.ShowMessage(Tag, "Like clicked!");
        }

        private void Comment()
        {
            if (_news.comments != null) ShowViewModel<CommentsViewModel>(_news.comments);
            ServiceBus.AlertService.ShowMessage(Tag, "Comment clicked!");
        }

        public void Init(string newId)
        {
            
            Title = _news.title;
            Subtitle = _news.authorId;
            Content = _news.body;
            if (_news.comments != null)
            {
                CommentsCount = _news.comments.Count;
            }
            if (_news.likes != null)
            {
                LikesCount = _news.likes.Count;
            }
        }
    }
}