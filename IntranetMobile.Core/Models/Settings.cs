using System;
namespace IntranetMobile.Core.Models
{
    public class Settings : Persist
    {
        
#region Notifications Settings

        public bool IsVibrationEnabled { get; set; }

        public bool IsNewsNotificationEnabled { get; set; }

        public bool IsReviewerNotificationEnabled { get; set; }

#endregion

    }
}

