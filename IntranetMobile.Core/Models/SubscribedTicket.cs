using System;
using System.Collections.Generic;

namespace IntranetMobile.Core
{
    public class SubscribedTicket
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public string DateReview { get; set; }
        public int OffersCount { get; set; }
        public PivotTicket Pivot { get; set; }

        public class PivotTicket
        {
            public string UserId { get; set; }
            public string ReviewRequestId { get; set; }
            public string IsAccepted { get; set; }
            public string Status { get; set; }
        }
    }
}

