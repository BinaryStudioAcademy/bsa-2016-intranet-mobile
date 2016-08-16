using System;
using System.Collections.ObjectModel;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels;

namespace IntranetMobile.Core
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<Comment> _comments = new ObservableCollection<Comment>
        {
            new Comment {Name = "name1", Body = "some text bla bla bla, more and more. I write comment!", Date = 140716},
            new Comment {Name = "name2", Body = "some text bla bla bla, more and more. I write comment!", Date = 140716},
            new Comment {Name = "name3", Body = "some text bla bla bla, more and more. I write comment!", Date = 140716}
        };

        public CommentsViewModel()
        {
            //_comments = GetListOfComments(compNewsDto);
        }

     /*   public ObservableCollection<Comment> GetListOfComments(CompNewsDto compNewsDto)
        {
            var listOfComments = new ObservableCollection<Comment>();

            foreach (var p in compNewsDto.comments)
            {
                var comment = new Comment();

                //TODO: Find Author Name
                comment.Body = p.body;
                comment.Date = p.date;
                comment.Countlikes = p.likes.Count;

                listOfComments.Add(comment);
            }

            return listOfComments;
        }*/

        public ObservableCollection<Comment> Comments
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

