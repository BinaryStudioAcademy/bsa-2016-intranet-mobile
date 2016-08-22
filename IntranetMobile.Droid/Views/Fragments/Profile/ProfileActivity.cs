using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Profile;
using IntranetMobile.Droid.Views.Activities;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Fragments.Profile
{
    [Activity(Theme = "@style/BSTheme")]
    public class ProfileActivity : BaseToolbarActivity<ProfileViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_profile;

        public override int ToolbarLayout { get; } = Resource.Id.activity_profile_toolbar;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }
    }
}