using MjCommerce.Shared.Models.Base;

namespace MjCommerce.Shared.Models.Orders
{
    public class OrderItem : EntityBase
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}