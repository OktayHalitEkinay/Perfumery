using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.Brand;
using Entities.Dtos.Order;
using Entities.Dtos.Perfume;

namespace Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, AddBrandDto>().ReverseMap();
            CreateMap<Perfume, AddPerfumeDto>().ReverseMap();
            CreateMap<Perfume, CartPerfumeDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
        }
    }
}
