namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TagViewModel : BaseViewModel
    {
        private string _tagName = string.Empty;

        public string TagName
        {
            get { return _tagName; }
            set
            {
                _tagName = value;
                RaisePropertyChanged(() => TagName);
            }
        }
    }
}