using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.News;

namespace IntranetMobile.Core
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<CommentsItemViewModel> _comments;

        public CommentsViewModel()
        {
           // _comments = GetListOfComments(comments);
            _comments = new ObservableCollection<CommentsItemViewModel>();
            _comments.Add(new CommentsItemViewModel("Name1", 17082016, "some body of comments", 2));
            _comments.Add(new CommentsItemViewModel("Name2", 17082016, "some body of comments", 2));
            _comments.Add(new CommentsItemViewModel("Name3", 17082016, "some body of comments", 2));
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
    }
}

