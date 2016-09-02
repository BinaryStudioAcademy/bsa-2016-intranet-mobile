namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TagViewModel : BaseViewModel
    {
        private string _tagName = "Tag!";

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