using MjCommerce.Shared.Models.Base;
using MjCommerce.Shared.Models.Identity;
using System.Collections.Generic;

namespace MjCommerce.Shared.Models.Orders
{
    public class Order : EntityBase
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}