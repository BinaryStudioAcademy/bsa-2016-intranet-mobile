using System;
namespace IntranetMobile.Core
{
    public class CommentTicketDto
    {
        public string id { get; set; }
        public string text { get; set; }
        public string created_at { get; set; }
        public string formatted_created_at { get; set; }
        public string updated_at { get; set; }
        public string deleted_at { get; set; }
        public string user_id { get; set; }
        public string review_request_id { get; set; }
        public UserTickets user { get; set; }
    }
}

