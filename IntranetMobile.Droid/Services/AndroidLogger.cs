using System;
using Android.Util;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Droid.Services
{
    public class AndroidLogger : ILogger
    {
        private const string Tag = "IntranetMobile"; 
        public void Error(Exception e)
        {
            Log.Error(Tag, e.ToString());
        }

        public void Error(string message)
        {
            Log.Error(Tag, message);
        }

        public void Error(string message, Exception e)
        {
            Log.Error(Tag, string.Format("{0}\n{1}", message, e));
        }

        public void Info(string message)
        {
            Log.Info(Tag, message);
        }
    }
}