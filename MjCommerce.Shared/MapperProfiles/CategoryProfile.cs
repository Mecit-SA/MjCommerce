using AutoMapper;
using MjCommerce.Shared.Models;

namespace MjCommerce.Shared.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, Category>();
        }
    }
}