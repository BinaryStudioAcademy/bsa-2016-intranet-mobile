namespace IntranetMobile.Core.Models.Dtos
{
    public class UserTicketDto
    {
        public string id { get; set; }
        public string bid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string job_id { get; set; }
        public string department_id { get; set; }
        public string binary_id { get; set; }
        public string thumb_avatar { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string job { get; set; }
        public string department { get; set; }
        public PivotDto pivot { get; set; }
    }

    public class PivotDto
    {
        public int review_request_id { get; set; }
        public int user_id { get; set; }
        public int isAccepted { get; set; }
        public string status { get; set; }
    }
}