using System;
using System.Collections.Generic;

namespace IntranetMobile.Core
{
    public class ReviewTicketsReqeustDto
    {
        public string title { get; set; }
        public string details { get; set; }
        public string date_review { get; set; }
        public string formatted_date_review { get; set; }
        public List<string> tags { get; set; }
        public string group { get; set; }
        public string created_at { get; set; }
        public string formatted_created_at { get; set; }
        public string id { get; set; }
        public string group_id { get; set; }
    }
}

