using MjCommerce.Shared.Models;
using AutoMapper;

namespace MjCommerce.Shared.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, Product>();
        }
    }
}
