using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DtoModels
{
    public class OrderDto
    {
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderID { get; set; }
        //public string CountryId { get; set; }
        //public CountryDto Country { get; set; }
        public SaleDto Sale { get; set; }
        //public DateTime FileDate { get; set; }
    }
}
