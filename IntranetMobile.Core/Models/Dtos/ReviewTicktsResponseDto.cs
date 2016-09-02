using System;
namespace IntranetMobile.Core
{
    public class ReviewTicktsResponseDto
    {
        public string title { get; set; }
        public string details { get; set; }
        public object date_review { get; set; }
        public int group_id { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
        public int offers_count { get; set; }
    }
}

