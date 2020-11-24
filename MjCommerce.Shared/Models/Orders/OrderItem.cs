using MjCommerce.Shared.Attributes;
using MjCommerce.Shared.Models.Base;

namespace MjCommerce.Shared.Models.Orders
{
    public class OrderItem : EntityBase
    {
        [MjRequired]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [MjRequired]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [MjRequired]
        public int Quantity { get; set; }
    }
}