using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<CommentsItemViewModel> _comments; 
        private string _commentBody;
        private CommentsResponseDto comments;

        private string _newsId;

        public CommentsViewModel()
        {
            ClickSaveCommentCommand = new MvxCommand(SaveCommentExecute);
        }

        public async void Init(Parameters arg)
        {
            _newsId = arg.NewsId;

            comments =  await ServiceBus.NewsService.GetListOfComments(arg.NewsId);
            GetListOfComments(comments, arg.NewsId);

            RaisePropertyChanged(() => Comments);

            /*comments = new CommentsResponseDto();

               CommentDto c = new CommentDto();

               c.authorId = "567abd6670a3a2541ae74c9a";
               c.date = 1453457840283;
               c.body = "<p>11</p>";
               c.likes = new List<string>{};

               _comments = new ObservableCollection<CommentsItemViewModel>
           {
                new CommentsItemViewModel(c, arg.NewsId),
               new CommentsItemViewModel(c, arg.NewsId),
               new CommentsItemViewModel(c, arg.NewsId),
               new CommentsItemViewModel(c, arg.NewsId)
           };*/
        }

        public void GetListOfComments(CommentsResponseDto comments, string newsId)
        {
            _comments = new ObservableCollection<CommentsItemViewModel>();

            if (comments == null) return;

            foreach (var c in comments.comments)
            {
                var comment = new CommentsItemViewModel(c, newsId);

                _comments.Add(comment);
            }

        }

        public ICommand ClickSaveCommentCommand { get; private set; }

        public string CommentBody
        {
            get { return _commentBody;}
            set
            {
                _commentBody = value;
                RaisePropertyChanged(() => CommentBody);
            }
        }

        public ObservableCollection<CommentsItemViewModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged(() => Comments);
            }
        }

        private void SaveCommentExecute()
        {
            CommentDto c = new CommentDto();

            c.body = CommentBody;
            c.authorId = ServiceBus.UserService.CurrentUser.UserId;
            c.date = DateTime.Now.Millisecond;
            c.commentId = "someId";
            c.likes = new List<string>();

            _comments.Add(new CommentsItemViewModel(c, _newsId));
            RaisePropertyChanged(() => Comments);

            CommentBody = "";
        }

        public class Parameters
        {
            public string NewsId { get; set; }
        }
    }
}

