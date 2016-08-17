﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.ViewModels;

namespace IntranetMobile.Core
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<CommentsItemViewModel> _comments;

        public CommentsViewModel(List<CommentDto> comments)
        {
            _comments = GetListOfComments(comments);
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
