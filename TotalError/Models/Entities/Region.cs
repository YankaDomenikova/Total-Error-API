using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Base;

namespace Models.Entities
{
    public class Region : BaseEntity
    {
        public string RegionName { get; set; }
        public List<Country> Countries { get; set; }
    }
}
