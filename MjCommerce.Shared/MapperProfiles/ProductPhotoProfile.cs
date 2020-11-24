using AutoMapper;
using MjCommerce.Shared.Models;

namespace MjCommerce.Shared.MapperProfiles
{
    public class ProductPhotoProfile : Profile
    {
        public ProductPhotoProfile()
        {
            CreateMap<ProductPhoto, ProductPhoto>();
        }
    }
}