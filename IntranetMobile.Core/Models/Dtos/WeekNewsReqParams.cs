// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class WeekNewsReqParams : Persist
    {
        public int skip { get; set; }
        public int limit { get; set; }
        public string published { get; set; }
    }
}