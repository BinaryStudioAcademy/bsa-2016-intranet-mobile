using Android.OS;
using Android.Runtime;
using IntranetMobile.Core.ViewModels;
using IntranetMobile.Core.ViewModels.Settings;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
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
    }
}

