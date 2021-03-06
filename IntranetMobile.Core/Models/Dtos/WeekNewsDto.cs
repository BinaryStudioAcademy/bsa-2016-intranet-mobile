﻿using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class WeekNewsDto : Persist
    {
        [JsonProperty("_id")]
        public string weekliesId { get; set; }

        public string title { get; set; }

        [JsonProperty("author")]
        public string authorId { get; set; }

        public long date { get; set; }

        public List<string> news { get; set; }

        public bool published { get; set; }

        public int __v { get; set; }

        public List<NewsDto> fullNews { get; set; }
    }
}