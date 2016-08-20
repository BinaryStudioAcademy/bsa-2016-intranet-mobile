using System;
using Android.App;
using IntranetMobile.Core;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "CommentsActivity", Theme = "@style/BSTheme")]
    public class CommentsActivity : BaseToolbarActivity<CommentsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_comments;
    }
}