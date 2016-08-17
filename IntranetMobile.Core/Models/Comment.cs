using System;
namespace IntranetMobile.Core
{
    public class Comment
    {
        public string Name { get; set; }

        public long Date { get; set; }

        public string Body { get; set; }

        public int Countlikes { get; set; }

        public string LikeImageViewUrl { get; set; }
    }
}

