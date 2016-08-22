using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Models.Dtos
{
    public class PositionDto
    {
        public List<object> Pdps { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Id { get; set; }
    }
}
