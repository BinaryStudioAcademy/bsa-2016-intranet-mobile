using IntranetMobile.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private const string Tag = "NewsDetailsViewModel";
        private string _content;
        private bool _isLiked;
        private string _subtitile;
        private string _title;

        public NewsDetailsViewModel()
        {
            LikeCommand = new MvxCommand(Like);
        }

        public MvxCommand LikeCommand { get; private set; }

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

        public void Init(NewsPreviewViewModel newsPreviewViewModel)
        {
            Title = newsPreviewViewModel.Title;
            Subtitle = newsPreviewViewModel.Subtitle;
            IsLiked = newsPreviewViewModel.IsLiked;
            Content =
                "<div><img style=\"display: block; margin-left: auto; margin-right: auto;\" src=\"https://gallery.mailchimp.com/d962b18774558cf34c062e6b3/images/5bd435e8-9528-4dff-be19-828845e44bab.jpg\" alt=\"\" width=\"599\" height=\"371\" /></div>\n<div>Что одеть на geek party?! Все просто - очки, подтяжки, нелепый галстук или бабочка, застегнутая до&nbsp;последней&nbsp;пуговицы&nbsp;рубашка, книги в руке, пиджак не по размеру, короткие брюки, небрежный хаер - и ты король этой вечеринки :) Хотим напомнить, что за лучший костюм предусмотрен крутейший подарок!&nbsp;<br /><br />Ждем тебя и твою вторую половинку&nbsp;25 декабря в нашем львовском офисе, где&nbsp;состоится празднование Нового Года и десятилетия компании!&nbsp;<br />Начало ивента в &nbsp;18.00 - <strong>если тебе позволяет проект и есть необходимость, то можно будет уйти с работы в 17.00</strong>, чтобы за час успеть съездить домой и переодеться.</div>\n<div><strong>Come to&nbsp;the geek side!</strong></div>";
        }
    }
}