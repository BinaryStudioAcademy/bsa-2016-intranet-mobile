using System;
using System.Collections.Generic;

namespace IntranetMobile.Core.Models
{
    public class WeeklyNews
    {
        public string WeeklyId { get; set; }

        public string AuthorId { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public List<string> SubNewsIdList { get; set; }

        public List<News> FullNews { get; set; }
    }
}
