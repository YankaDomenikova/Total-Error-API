using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Base;

namespace Models.Entities
{
    public class Order : BaseEntity
    {
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderID { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }
        public Sale Sale { get; set; }
        public DateTime FileDate { get; set; }
    }
}
