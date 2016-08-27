using System;
using Android.Content;
using Android.Preferences;
using Android.Util;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Droid.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly Context _context;

        public SettingsService(Context context)
        {
            _context = context;
        }

        public Settings GetSettings()
        {
            var settings = new Settings();

            var manager = PreferenceManager.GetDefaultSharedPreferences(_context);

            settings.IsVibrationEnabled = manager.GetBoolean(
                                _context.GetString(Resource.String.pref_vibration), true);

            settings.IsNewsNotificationEnabled = manager.GetBoolean(
                                _context.GetString(Resource.String.pref_news), true);

            settings.IsReviewerNotificationEnabled = manager.GetBoolean(
                                _context.GetString(Resource.String.pref_reviewer), true);

            return settings;
        }

        public bool SaveSettings(Settings settings)
        {
            try
            {
                var manager = PreferenceManager.GetDefaultSharedPreferences(_context);
                var editor = manager.Edit();
                editor.PutBoolean(_context.GetString(Resource.String.pref_vibration),
                                  settings.IsVibrationEnabled);
                editor.PutBoolean(_context.GetString(Resource.String.pref_news),
                                  settings.IsNewsNotificationEnabled);
                editor.PutBoolean(_context.GetString(Resource.String.pref_reviewer),
                                  settings.IsReviewerNotificationEnabled);
                editor.Commit();

                return true;
            }
            catch(Exception e)
            {
                Log.Error("Intranet.SettingsService", e.ToString());
                return false;
            }
        }
    }
}

