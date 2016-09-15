using System;
using System.Collections.Generic;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class WeekliesDetailsActivity : BaseToolbarActivity<WeekliesDetailsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_weeklies_details;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            //var list = FindViewById<MvxListView>(Resource.Id.weeklies_recycler_view);
            //list.Adapter = new WeekliesAdapter(this, (IMvxAndroidBindingContext) BindingContext);
        }
    }

    public class WeekliesAdapter : MvxAdapter
    {
        private List<Item> _itemList;

        private int _prevPosition;

        public WeekliesAdapter(Context context, IMvxAndroidBindingContext bindingContext)
            : base(context, bindingContext)
        {
            _prevPosition = -1;
            _itemList = new List<Item>();
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);

            if (_itemList.Count == position)
            {
                var content = view.FindViewById<LinearLayout>(Resource.Id.weeklyDetailsContent);
                content.Visibility = Android.Views.ViewStates.Gone;

                var button = view.FindViewById<MvxImageView>(Resource.Id.weeklyDetailsExpanderButton);
                button.Click += (sender, e) => ExpandDetailsItem(position);
                var arrowDown = Context.GetDrawable(Resource.Drawable.ic_keyboard_arrow_down_black_24dp);
                button.SetImageDrawable(arrowDown);

                _itemList.Add(new Item { Content = content, Button = button });
            }

            return view;
        }

        private void ExpandDetailsItem(int position)
        {
            var arrowDown = Context.GetDrawable(Resource.Drawable.ic_keyboard_arrow_down_black_24dp);
            var arrowUp = Context.GetDrawable(Resource.Drawable.ic_keyboard_arrow_up_black_24dp);

            var currentItem = _itemList[position];
            if (position == _prevPosition && currentItem.Content.Visibility == ViewStates.Visible)
            {
                currentItem.Content.Visibility = ViewStates.Gone;
                currentItem.Button.SetImageDrawable(arrowDown);
                _prevPosition = -1;
                return;
            }

            if (_prevPosition >= 0)
            {
                var prevItem = _itemList[_prevPosition];
                prevItem.Content.Visibility = ViewStates.Gone;
                prevItem.Button.SetImageDrawable(arrowDown);
            }

            currentItem.Content.Visibility = ViewStates.Visible;
            currentItem.Button.SetImageDrawable(arrowUp);
            _prevPosition = position;
        }

        private struct Item
        {
            public MvxImageView Button { get; set; }

            public LinearLayout Content { get; set; }
        }
    }
}