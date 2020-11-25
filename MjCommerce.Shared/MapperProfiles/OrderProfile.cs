using AutoMapper;
using MjCommerce.Shared.Models.Orders;

namespace MjCommerce.Shared.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, Order>();
        }
    }
}