using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper.Configuration.Attributes;

namespace Data.Services.TransferModel
{
    public class FileTransferModel
    {
        [Name("Region")]
        public string RegionName { get; set; }

        [Name("Country")]
        public string CountryName { get; set; }

        [Name("Item Type")]
        public string ItemType { get; set; }

        [Name("Sales Channel")]
        public string SalesChannel { get; set; }

        [Name("Order Priority")]
        public string OrderPriority { get; set; }

        [Name("Order Date")]
        public DateTime OrderDate { get; set; }

        [Name("Order ID")]
        public string OrderID { get; set; }

        [Name("Ship Date")]
        public DateTime ShipDate { get; set; }

        [Name("Units Sold")]
        public int UnitsSold { get; set; }

        [Name("Unit Price")]
        public double UnitPrice { get; set; }

        [Name("Unit Cost")]
        public double UnitCost { get; set; }

        [Name("Total Revenue")]
        public double TotalRevenue { get; set; }

        [Name("Total Cost")]
        public double TotalCost { get; set; }

        [Name("Total Profit")]
        public double TotalProfit { get; set; }
    }
}
