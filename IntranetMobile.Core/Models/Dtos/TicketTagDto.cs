namespace IntranetMobile.Core.Models.Dtos
{
    public class TicketTagDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public int requests_count { get; set; }
        public PivotTicketsDto pivot { get; set; }
    }

    public class PivotTicketsDto
    {
        public string review_request_id { get; set; }
        public string tag_id { get; set; }
    }
}

