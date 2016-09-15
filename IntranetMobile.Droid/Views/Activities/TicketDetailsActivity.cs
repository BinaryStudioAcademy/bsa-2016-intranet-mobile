using Android.App;
using Android.Content.PM;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core.ViewModels.Reviewer;
using IntranetMobile.Droid.Views.Util;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme", LaunchMode = LaunchMode.SingleTop)]
    public class TicketDetailsActivity : BaseToolbarActivity<TicketDetailsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_ticket_details;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var tagsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.activity_ticket_details_tags);
            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);

            tagsRecyclerView.SetLayoutManager(layoutManager);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_ticket_details_comment:
                    {
                        ViewModel.CommentCommand.Execute();
                        break;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_ticket_details, menu);

            var commentCountItem = menu.FindItem(Resource.Id.menu_reviewer_details_comments_text_item);
            var commentCountTextView = commentCountItem.ActionView.FindViewById<TextView>
                                                       (Resource.Id.menu_reviewer_comments_textview);
            
            var bindingSet = this.CreateBindingSet<TicketDetailsActivity, TicketDetailsViewModel>();
            bindingSet.Bind(commentCountTextView)
                .For(c => c.Text)
                .To(viewModel => viewModel.CommentsCount);
            bindingSet.Apply();

            return base.OnCreateOptionsMenu(menu);
        }
    }
}