using System;
using System.Collections.Generic;

namespace IntranetMobile.Core
{
    public class SubscribedTicketDto
    {
        public List<Message> message { get; set; }

        public class Pivot
        {
            public string user_id { get; set; }
            public string review_request_id { get; set; }
            public string isAccepted { get; set; }
            public string status { get; set; }
        }

        public class Message
        {
            public string id { get; set; }
            public string title { get; set; }
            public string details { get; set; }
            public string created_at { get; set; }
            public string user_id { get; set; }
            public string group_id { get; set; }
            public string date_review { get; set; }
            public int offers_count { get; set; }
            public Pivot pivot { get; set; }
        }
    }
}

