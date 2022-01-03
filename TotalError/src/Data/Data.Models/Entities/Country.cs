using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Base;

namespace Models.Entities
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }
        public string RegionId { get; set; }
        public Region Region { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
