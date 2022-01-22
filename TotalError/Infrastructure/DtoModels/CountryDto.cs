using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DtoModels
{
    public class CountryDto
    {
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        //public string RegionId { get; set; }
        //public RegionDto Region { get; set; }
        public int OrdersCount { get; set; }
        public double TotalRevenue{ get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; }
        public int UnitsSold { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
