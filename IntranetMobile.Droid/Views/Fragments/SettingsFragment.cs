using System;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.Preferences;
using Android.Support.V7.Widget;
using Android.Views;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Settings;
using IntranetMobile.Droid.Views.Activities;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Preference;

namespace IntranetMobile.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("intranetmobile.droid.views.fragments.SettingsFragment")]
    public class SettingsFragment : BaseDrawerFragment<SettingsViewModel>
    {
        public override int ToolbarLayout { get; protected set; } = Resource.Id.mvx_toolbar;

        public override int FragmentLayout { get; protected set; } = Resource.Layout.fragment_settings;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FragmentManager.BeginTransaction()
                           .Replace(Resource.Id.preference_content_frame, new SettingsPreference())
                           .Commit();
        }
    }

    public class SettingsPreference : MvxPreferenceFragmentCompat
    {
        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            this.AddPreferencesFromResource(Resource.Xml.preferences);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            //var serverPreference = (EditTextPreference)this.FindPreference("pref_server");

            //var bindingSet = this.CreateBindingSet<SettingsFragment, SettingsViewModel>();
            //bindingSet.Bind(serverPreference)
            //    .For(v => v.Text)
            //    .To(vm => vm.ServerAddress);
            //bindingSet.Apply();

            return view;
        }
    }
}

