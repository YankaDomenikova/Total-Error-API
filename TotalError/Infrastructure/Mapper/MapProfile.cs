using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Infrastructure.DtoModels;

using Models.Entities;

namespace Infrastructure.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
        }
    }
}
