using System;
using System.Collections.Generic;

namespace IntranetMobile.Core
{
    public class TicketsDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public string created_at { get; set; }
        public string user_id { get; set; }
        public string group_id { get; set; }
        public string date_review { get; set; }
        public int offers_count { get; set; }
        public UserTickets user { get; set; }
        public GroupTickets group { get; set; }
        public List<TagTickets> tags { get; set; }
        public List<UserTickets> users { get; set; }

        public class GroupTickets
        {
            public string id { get; set; }
            public string title { get; set; }
        }
    }
}

