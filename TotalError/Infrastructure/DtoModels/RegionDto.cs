using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DtoModels
{
    public class RegionDto
    {
        public string RegionName { get; set; }
        public List<CountryDto> Countries { get; set; }
    }
}
