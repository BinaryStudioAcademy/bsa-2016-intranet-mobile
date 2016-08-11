using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class ItemNewsViewModel : BaseViewModel
    {
        private string coverImageViewUrl;
        public ICommand ClickLikeCommand { get; private set; }
        private string likeImageViewUrl;
        private bool _isLiked;

        public ItemNewsViewModel()
        {
            ClickLikeCommand = new MvxCommand(ClickLikeCommandExecute);
        }

        private void ClickLikeCommandExecute()
        {
            IsLiked = !_isLiked;
        }

        public string CoverImageViewUrl {
            get
            {
                if (string.IsNullOrEmpty(coverImageViewUrl))
                {
                    coverImageViewUrl = "logo_black_1";
                }
                return coverImageViewUrl;
            }
            set
            {
                coverImageViewUrl = value;
                RaisePropertyChanged(()=> CoverImageViewUrl);
            }
        }

        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;
                LikeImageViewUrl = _isLiked ? "ic_favorite_white_24dp" : "ic_favorite_border_white_24dp";
            }
        }

        public string LikeImageViewUrl {
            get
            {
                if (string.IsNullOrEmpty(likeImageViewUrl))
                {
                    likeImageViewUrl = "ic_favorite_border_white_24dp";
                }
                return likeImageViewUrl;
            }
            set
            {
                likeImageViewUrl = value; 
                RaisePropertyChanged(()=> LikeImageViewUrl);
            }
        }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string NewsUrl { get; set; }
    }
}
