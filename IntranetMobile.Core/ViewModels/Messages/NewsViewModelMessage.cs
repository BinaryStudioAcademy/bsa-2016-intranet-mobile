using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Plugins.Messenger;

namespace IntranetMobile.Core.ViewModels.Messages
{
    public class NewsViewModelMessage : MvxMessage
    {
        public NewsViewModelMessage(object sender, NewsViewModel newsViewModel) : base(sender)
        {
            ViewModel = newsViewModel;
        }

        public NewsViewModel ViewModel { get; }
    }
}
