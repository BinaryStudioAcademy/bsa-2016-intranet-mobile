using System.Collections.Generic;

namespace IntranetMobile.Core.Models.Dtos
{
    public class TicketDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public string created_at { get; set; }
        public string user_id { get; set; }
        public string group_id { get; set; }
        public string date_review { get; set; }
        public int offers_count { get; set; }
        public UserTicketDto user { get; set; }
        public TicketGroupDto group { get; set; }
        public List<TicketTagDto> tags { get; set; }
        public List<UserTicketDto> users { get; set; }
    }

    public class TicketGroupDto
    {
        public string id { get; set; }
        public string title { get; set; }
    }
}