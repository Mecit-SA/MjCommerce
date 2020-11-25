using AutoMapper;
using MjCommerce.Shared.Models.Orders;
using MjCommerce.Shared.Repositories.Base;

namespace MjCommerce.Shared.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(MjCommerceDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}