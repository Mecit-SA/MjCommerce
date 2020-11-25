using MjCommerce.Shared.Filters.Base;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models.Orders;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class OrderFilter : PagingFilter<Order>, IFilter<Order>
    {
        public string CustomerId { get; set; }

        public IQueryable<Order> Build(IQueryable<Order> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(o => o.Active == Active);
            }

            if (!string.IsNullOrWhiteSpace(CustomerId))
            {
                initialSet = initialSet.Where(o => o.CustomerId.Equals(CustomerId));
            }

            if (applyPaging)
            {
                return Build(initialSet);
            }

            return initialSet;
        }

        public override string ToString()
        {
            return ToQueryString(GetType().GetProperties());
        }
    }
}