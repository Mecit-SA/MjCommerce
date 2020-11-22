using System.Collections.Generic;

namespace MjCommerce.Shared.Models.Identity
{
    public class Seller : User
    {
        public ICollection<Product> Products { get; set; }
    }
}