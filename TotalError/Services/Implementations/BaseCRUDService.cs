using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;

using Data;

using Infrastructure.DtoModels;
using Infrastructure.Interfaces;

using Models.Entities;

namespace Services.Implementations
{
    public class BaseCRUDService : IBaseCRUD
    {
        public BaseCRUDService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void ReadFile(string dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir);

            List<FileTransferModel> records = new List<FileTransferModel>();

            var lastReadedDate = DbContext.LastReadedFiles.OrderByDescending(x => x.LastReaded);

            foreach (string currentFile in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(currentFile);

                if (lastReadedDate.Count() > 0)
                {
                    if (lastReadedDate.First().LastReaded < DateTime.Parse(fileName))
                    {
                        using (var reader = new StreamReader(currentFile))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            records = csv.GetRecords<FileTransferModel>().ToList();
                        }
                        DbContext.LastReadedFiles.Add(new LastReadedFile() { LastReaded = DateTime.Parse(fileName) });
                        SaveToDB(records, fileName);

                    }
                    DbContext.SaveChanges();

                }
                else
                {
                    using (var reader = new StreamReader(currentFile))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        records = csv.GetRecords<FileTransferModel>().ToList();
                    }
                    DbContext.LastReadedFiles.Add(new LastReadedFile() { LastReaded = DateTime.Parse(fileName) });
                    SaveToDB(records, fileName);
                    DbContext.SaveChanges();
                }

            }


        }

        public void SaveToDB(List<FileTransferModel> records, string fileName)
        {
            Region currentRegion;

            for (int i = 0; i < records.Count(); i++)
            {
                currentRegion = DbContext.Regions.FirstOrDefault(x => x.RegionName.ToUpper() == records[i].RegionName.ToUpper());

                if (currentRegion == null)
                {
                    Region newRegion = new Region();
                    newRegion.RegionName = records[i].RegionName;
                    newRegion.Countries = new List<Country>();

                    Country country = new Country();
                    country.CountryName = records[i].CountryName;
                    country.Orders = new List<Order>();

                    Order order = new Order();
                    order.ItemType = records[i].ItemType;
                    order.SalesChannel = records[i].SalesChannel;
                    order.OrderPriority = records[i].OrderPriority;
                    order.OrderDate = records[i].OrderDate;
                    order.OrderID = records[i].OrderID;
                    order.FileDate = DateTime.Parse(fileName);
                    order.Sale = new Sale()
                    {
                        ShipDate = records[i].ShipDate,
                        UnitsSold = records[i].UnitsSold,
                        UnitPrice = records[i].UnitPrice,
                        UnitCost = records[i].UnitCost,
                        TotalRevenue = records[i].TotalRevenue,
                        TotalCost = records[i].TotalCost,
                        TotalProfit = records[i].TotalProfit,
                        FileDate = DateTime.Parse(fileName)
                    };
                    country.Orders.Add(order);
                    newRegion.Countries.Add(country);
                    DbContext.Regions.Add(newRegion);
                    DbContext.SaveChanges();

                }
                else
                {
                    Country currentCountry = DbContext.Countries.FirstOrDefault(x => x.CountryName.ToUpper() == records[i].CountryName.ToUpper());

                    if (currentCountry == null)
                    {
                        Country newCountry = new Country();
                        newCountry.CountryName = records[i].CountryName;
                        newCountry.RegionId = currentRegion.Id;
                        newCountry.Orders = new List<Order>();

                        Order order = new Order();
                        order.ItemType = records[i].ItemType;
                        order.SalesChannel = records[i].SalesChannel;
                        order.OrderPriority = records[i].OrderPriority;
                        order.OrderDate = records[i].OrderDate;
                        order.OrderID = records[i].OrderID;
                        order.FileDate = DateTime.Parse(fileName);
                        order.Sale = new Sale()
                        {
                            ShipDate = records[i].ShipDate,
                            UnitsSold = records[i].UnitsSold,
                            UnitPrice = records[i].UnitPrice,
                            UnitCost = records[i].UnitCost,
                            TotalRevenue = records[i].TotalRevenue,
                            TotalCost = records[i].TotalCost,
                            TotalProfit = records[i].TotalProfit,
                            FileDate = DateTime.Parse(fileName)
                        };
                        newCountry.Orders.Add(order);
                        DbContext.Countries.Add(newCountry);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        Order order = new Order();
                        order.CountryId = currentCountry.Id;
                        order.ItemType = records[i].ItemType;
                        order.SalesChannel = records[i].SalesChannel;
                        order.OrderPriority = records[i].OrderPriority;
                        order.OrderDate = records[i].OrderDate;
                        order.OrderID = records[i].OrderID;
                        order.FileDate = DateTime.Parse(fileName);
                        order.Sale = new Sale()
                        {
                            ShipDate = records[i].ShipDate,
                            UnitsSold = records[i].UnitsSold,
                            UnitPrice = records[i].UnitPrice,
                            UnitCost = records[i].UnitCost,
                            TotalRevenue = records[i].TotalRevenue,
                            TotalCost = records[i].TotalCost,
                            TotalProfit = records[i].TotalProfit,
                            FileDate = DateTime.Parse(fileName)
                        };
                        DbContext.Orders.Add(order);
                        DbContext.SaveChanges();
                    }
                }
            }

        }
    }
}
