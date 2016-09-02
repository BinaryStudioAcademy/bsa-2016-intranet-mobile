using System.Collections.ObjectModel;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class TicketDetailsViewModel : BaseViewModel
    {
        public TicketDetailsViewModel()
        {
            Title = "Accounting code review";

            Tags.Add(new TagViewModel {TagName = "regex"});
            Tags.Add(new TagViewModel {TagName = "nodejs"});
            Tags.Add(new TagViewModel {TagName = "string"});
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());
            Tags.Add(new TagViewModel());

            Offers.Add(new TicketOfferViewModel(ServiceBus.UserService.CurrentUser.UserId));
            Offers.Add(new TicketOfferViewModel(ServiceBus.UserService.CurrentUser.UserId));
            Offers.Add(new TicketOfferViewModel(ServiceBus.UserService.CurrentUser.UserId));
            Offers.Add(new TicketOfferViewModel(ServiceBus.UserService.CurrentUser.UserId));
            Offers.Add(new TicketOfferViewModel(ServiceBus.UserService.CurrentUser.UserId));
        }

        public string AuthorName { get; } = "AuthorName";

        public string CategoryName { get; } = "JS";

        public string TicketText { get; } =
            @"Note that the variables are in scope in the enclosing block, so the subsequent line can use them. Most kinds of statements do not establish their own scope, so out variables declared in them are usually introduced into the enclosing scope."
            ;

        public string ReviewDate { get; } = "December 24, 2015, 23:20";

        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();

        public ObservableCollection<TicketOfferViewModel> Offers { get; } = new ObservableCollection<TicketOfferViewModel>();
    }
}