﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ItemReviewViewModel : BaseViewModel
    {
        private string _authorImage;
        private string _titleName;
        private string _author;
        private string _dateTime;

        public string AuthorImage
        {
            get { return _authorImage; }
            set
            {
                _authorImage = value;
                RaisePropertyChanged(()=> AuthorImage);
            }
        }

        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value; 
                RaisePropertyChanged(()=> TitleName);
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RaisePropertyChanged(()=> Author);
            }
        }

        public string DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value; 
                RaisePropertyChanged(()=>DateTime);
            }
        }
    }
}
