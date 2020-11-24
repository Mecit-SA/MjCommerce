using AutoMapper;
using MjCommerce.Shared.Models;

namespace MjCommerce.Shared.MapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, City>();
        }
    }
}