using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IntranetMobile.Droid
{
    [Activity(Label = "CommentsActivity", Theme = "@style/BSTheme")]
    public class CommentsActivity : MvxAppCompatActivity<CommentsViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.activity_comments);
        }
    }
}