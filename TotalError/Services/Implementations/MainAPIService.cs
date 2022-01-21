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
            //var regions = DbContext.Regions.Include(x => x.Countries).ThenInclude(x => x.Orders).ToList();
            var res = Mapper.Map<List<RegionDto>>(DbContext.Regions.ToList());
            return res;
        }
        public List<CountryDto> GetCountriesByRegion(string id)
        {
            var res = DbContext.Countries.Where(x => x.RegionId == id).ToList();
            var mapped = Mapper.Map<List<CountryDto>>(res);
            return mapped;
        }

        public CountryDto GetCountryByName(string name)
        {
            var res = DbContext.Countries.FirstOrDefault(x => x.CountryName.ToLower() == name.ToLower());
            var mapped = Mapper.Map<CountryDto>(res);
            return mapped;
        }
    }
}
