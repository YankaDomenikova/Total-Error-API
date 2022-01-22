using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.DtoModels;

namespace Infrastructure.Interfaces
{
    public interface IMainAPIService
    {
        public List<RegionDto> GetRegions();
        public List<CountryDto> GetAllCountries();
        public List<CountryDto> GetCountriesByRegion(string id);
        public CountryDto GetCountryByName(string name);
        public List<CountryDto> GetCountriesByTotalProfit();
        public double GetTotalCostOfCountry(string name);
    }
}
