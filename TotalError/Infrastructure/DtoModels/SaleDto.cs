using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DtoModels
{
    public class SaleDto
    {
        public DateTime ShipDate { get; set; }
        public int UnitsSold { get; set; }
        public double UnitPrice { get; set; }
        public double UnitCost { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; }
        public string OrderId { get; set; }
        public OrderDto Order { get; set; }
        public DateTime FileDate { get; set; }
    }
}
