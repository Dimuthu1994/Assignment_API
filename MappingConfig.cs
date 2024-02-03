using Assignment_API.Models;
using Assignment_API.Models.Dto;
using AutoMapper;

namespace Assignment_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ForMember(dest => dest.ProductId, opt => opt.Ignore());
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
