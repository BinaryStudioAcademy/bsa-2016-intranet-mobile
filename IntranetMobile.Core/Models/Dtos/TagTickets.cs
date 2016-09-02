using System;
namespace IntranetMobile.Core
{
    public class TagTickets
    {
        public string id { get; set; }
        public string title { get; set; }
        public int requests_count { get; set; }
        public PivotTickets pivot { get; set; }

        public class PivotTickets
        {
            public string review_request_id { get; set; }
            public string tag_id { get; set; }
        } 
    }
}

