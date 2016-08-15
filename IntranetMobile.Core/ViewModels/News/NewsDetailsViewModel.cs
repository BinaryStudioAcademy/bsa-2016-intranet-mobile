namespace IntranetMobile.Core.ViewModels.News
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        private string _title;
        private string _subtitile;

        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value; 
                RaisePropertyChanged(() => Title);
            }
        }

        public string Subtitile
        {
            get { return _subtitile; }
            private set
            {
                _subtitile = value; 
                RaisePropertyChanged(() => Subtitile);
            }
        }

        public void Init(ItemNewsViewModel itemNewsViewModel)
        {
            Title = itemNewsViewModel.Title;
            Subtitile = itemNewsViewModel.Subtitle;
        }
    }
}