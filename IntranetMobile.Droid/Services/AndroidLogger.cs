using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core;
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