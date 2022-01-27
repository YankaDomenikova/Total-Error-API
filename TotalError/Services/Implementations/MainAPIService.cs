using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Data;

using Infrastructure.DtoModels;
using Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Services.Implementations
{
    public class MainAPIService : IMainAPIService
    {
        
        public MainAPIService(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public ApplicationDbContext DbContext { get; }
        public IMapper Mapper { get; }

        public List<RegionDto> GetRegions()
        {
            var res = DbContext.Regions
                .Include(x => x.Countries)
                .Select(x => new RegionDto()
                {
                    RegionName = x.RegionName,
                    //Countries = x.Countries.Select(z => new CountryDto() { CountryName = z.CountryName }).ToList()
                })
                .ToList();

            var mapped = Mapper.Map <List<RegionDto>>(res);
            return mapped;
        }

        public List<CountryDto> GetAllCountries()
        {
            var res = DbContext.Countries
                .Include(x => x.Orders)
                .Select(x => new CountryDto() 
                { 
                    CountryName = x.CountryName,
                    RegionName = x.Region.RegionName,
                    OrdersCount = x.Orders.Count()
                })
                .ToList();
            var mapped = Mapper.Map<List<CountryDto>>(res);
            return mapped;
            
        }
        public List<CountryDto> GetCountriesByRegion(string id)
        {
            var res = DbContext.Countries
                .Where(x => x.RegionId == id)
                .Select(x => new CountryDto() { 
                    CountryName = x.CountryName,
                    RegionName = x.Region.RegionName
                })
                .ToList();
            var mapped = Mapper.Map<List<CountryDto>>(res);
            return mapped;
        }

        public CountryDto GetCountryByName(string name)
        {
            var res = DbContext.Countries
                .Where(x => x.CountryName.ToUpper() == name.ToUpper())
                .Select(x => new CountryDto()
                {
                    CountryName = x.CountryName,
                    RegionName = x.Region.RegionName,
                    OrdersCount = x.Orders.Count()
                })
                .FirstOrDefault();

            var mapped = Mapper.Map<CountryDto>(res);
            return mapped;
        }

        public List<CountryDto> GetCountriesByTotalProfit()
        {
            var res = DbContext.Countries
                .Include(x => x.Orders)
                .ThenInclude(x => x.Sale)
                .Select(x => new CountryDto()
                {
                    CountryName = x.CountryName,
                    TotalProfit = Math.Round(x.Orders.Sum(z => z.Sale.TotalProfit), 2),
                    UnitsSold = x.Orders.Select(z => z.Sale.UnitsSold).Sum()
                })
                .OrderByDescending(x => x.TotalProfit)
                .ToList();

            var mapped = Mapper.Map<List<CountryDto>>(res);
            return mapped;
        }

        public double GetTotalCostOfCountry(string name)
        {
            var res = DbContext.Countries
                .Where(x => x.CountryName.ToUpper() == name.ToUpper())
                .Include(x => x.Orders)
                .ThenInclude(x => x.Sale)
                .Select(x => new CountryDto()
                {
                    TotalCost = Math.Round(x.Orders.Sum(z => z.Sale.TotalCost), 2)
                }).FirstOrDefault();

            return res.TotalCost;
        }
    }
}
