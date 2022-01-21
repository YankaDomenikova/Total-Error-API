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
            
            baseCRUD.ReadFile(DirectoryConstant.Directory);


            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Region, RegionDto>());
            //var mapper = new Mapper(config);

            //MainAPIService mainAPIService = new MainAPIService(new ApplicationDbContext(), mapper);
            //var res = mainAPIService.GetRegions();
        }
    }
}
