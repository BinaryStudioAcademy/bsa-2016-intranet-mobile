using System;
using Android.Runtime;
using IntranetMobile.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.AsciitFragment")]
    public class AsciitFragment : BaseDrawerFragment<AsciitViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.mvx_toolbar;

        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_asciit;
    }
}

