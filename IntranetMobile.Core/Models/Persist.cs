using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace IntranetMobile.Core.Models
{
    public abstract class Persist
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int LocalId { get; set; }
    }
}