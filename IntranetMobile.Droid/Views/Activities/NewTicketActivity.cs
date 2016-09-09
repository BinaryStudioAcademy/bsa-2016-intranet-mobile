using Android.App;
using IntranetMobile.Core;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "NewTicketActivity",
              Theme = "@style/BSTheme")]
    public class NewTicketActivity : BaseToolbarActivity<NewTicketViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_new_ticket;
    }
}

