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
using IntranetMobile.Core.ViewModels.News;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Theme = "@style/BSTheme")]
    public class WeekliesDetailsActivity : BaseToolbarActivity<WeekliesDetailsViewModel>
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_weeklies_details;
    }
}