using System;
using Android.Util;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Droid.Services
{
    public class AndroidLogger : ILogger
    {
        public void Error(Exception e)
        {
        }

        public void Error(string message)
        {
            Log.Error("Error", message);
        }

        public void Error(string message, Exception e)
        {
        }

        public void Info(string message)
        {
            Log.Info("Info", message);
        }
    }
}