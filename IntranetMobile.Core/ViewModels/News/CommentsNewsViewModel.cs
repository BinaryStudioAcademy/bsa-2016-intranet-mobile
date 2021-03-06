﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CommentsNewsViewModel : BaseViewModel
    {
        private string _newComment;
        private string _newsId;

        public CommentsNewsViewModel()
        {
            Comments = new ObservableCollection<CommentsNewsItemViewModel>();
            SendCommentCommand = new MvxCommand(SendCommentExecute);
            Title = "Comments";
        }

        public void Init(Parameters arg)
        {
            _newsId = arg.NewsId;
            GetComments();
        }

        public async void GetComments()
        {
            IsBusy = true;
            try
            {
                var data = await ServiceBus.NewsService.LoadListOfCommentsAsync(_newsId);
                if (data == null) return;

                Comments.Clear();
                foreach (var c in data.comments)
                {
                    var comment = new CommentsNewsItemViewModel(c, _newsId);
                    Comments.Add(comment);
                }
            }
            catch
            {
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }

            IsBusy = false;
        }

        public ICommand SendCommentCommand { get; private set; }

        public string NewComment
        {
            get { return _newComment;}
            set
            {
                _newComment = value;
                RaisePropertyChanged(() => NewComment);
            }
        }

        public ObservableCollection<CommentsNewsItemViewModel> Comments { get; private set; }

        private async void SendCommentExecute()
        {
            if (string.IsNullOrWhiteSpace(NewComment))
                return;
            try
            {
                await ServiceBus.NewsService.AddCommentAsync(ServiceBus.UserService.CurrentUser.ServerId, NewComment, _newsId);
                NewComment = "";
                GetComments();
            }
            catch
            {
                ServiceBus.AlertService.ShowConnectionLostMessage();
            }
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}

