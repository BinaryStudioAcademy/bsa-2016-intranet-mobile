﻿// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class CompNewsReqParams : Persist
    {
        public string type { get; set; }

        public int limit { get; set; }

        public int skip { get; set; }
    }
}