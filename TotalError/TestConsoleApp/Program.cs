using System.Threading.Tasks;

using AutoMapper;

using Data;

using Infrastructure.Constants;
using Infrastructure.DtoModels;

using Models.Entities;

using Services.Implementations;

namespace TestConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            BaseCRUDService baseCRUD = new BaseCRUDService(new ApplicationDbContext());
            
            await baseCRUD.ReadFile(DirectoryConstant.Directory);


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Region, RegionDto>();
                cfg.CreateMap<Country, CountryDto>();
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Sale, SaleDto>();
            });
            var mapper = new Mapper(config);

            MainAPIService mainAPIService = new MainAPIService(new ApplicationDbContext(), mapper);
            //var res = mainAPIService.GetRegions();
             //var res = mainAPIService.GetCountriesByRegion("a04378b7-710b-45cd-a872-471bf58bf1e4");
            //var res = mainAPIService.GetAllCountries();
        }
    }
}
