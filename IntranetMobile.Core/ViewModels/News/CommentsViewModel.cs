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
        private List<Comment> _listOfComments;

        public CommentsViewModel()
        {
            _comments = new ObservableCollection<CommentsItemViewModel>
            {
                new CommentsItemViewModel("Name1", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name2", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name3", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name4", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name5", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name6", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name7", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name8", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name9", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name10", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name11", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name12", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name13", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name14", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name15", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name16", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name17", 17082016, "some body of comments", 2),
                new CommentsItemViewModel("Name18", 17082016, "some body of comments", 2)
            };

            //_comments = GetListOfComments(_listOfComments);

            ClickSaveCommentCommand = new MvxCommand(SaveCommentExecute);
               
        }

        public async void LoadNews(string newsId)
        {
            var news = await ServiceBus.NewsService.GetCompanyNewsById(newsId);
            _listOfComments = news.Comments;
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

        public ObservableCollection<CommentsItemViewModel> GetListOfComments(List<CommentDto> comments)
        {
            var listOfComments = new ObservableCollection<CommentsItemViewModel>();

            foreach (var p in comments)
            {
                var comment = new CommentsItemViewModel(p.authorId, p.date, p.body, p.likes.Count);

                listOfComments.Add(comment);
            }

            return listOfComments;
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
            //TODO:saveComment
            CommentBody = "";
        }
    }
}

