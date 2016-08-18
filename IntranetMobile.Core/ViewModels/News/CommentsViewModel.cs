using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<CommentsItemViewModel> _comments;
        private string _commentBody;

        public CommentsViewModel()
        {
           
                _comments = new ObservableCollection<CommentsItemViewModel>();
                _comments.Add(new CommentsItemViewModel("Name1", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name2", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name3", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name4", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name5", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name6", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name7", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name8", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name9", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name10", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name11", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name12", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name13", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name14", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name15", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name16", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name17", 17082016, "some body of comments", 2));
                _comments.Add(new CommentsItemViewModel("Name18", 17082016, "some body of comments", 2));

                ClickSaveCommentCommand = new MvxCommand(SaveCommentExecute);
               
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

