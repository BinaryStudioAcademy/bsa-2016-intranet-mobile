using Android.App;
using IntranetMobile.Core.ViewModels.Profile;
using IntranetMobile.Core.ViewModels.Reviewer;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class TicketDetailsActivity : BaseToolbarActivity<TicketDetailsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_ticket_details;
    }
}