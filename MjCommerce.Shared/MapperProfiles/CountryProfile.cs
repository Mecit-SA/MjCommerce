using AutoMapper;
using MjCommerce.Shared.Models;

namespace MjCommerce.Shared.MapperProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, Country>();
        }
    }
}