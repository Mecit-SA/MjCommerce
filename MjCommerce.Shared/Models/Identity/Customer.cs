using System.Collections.Generic;

namespace MjCommerce.Shared.Models.Identity
{
    public class Customer : User
    {
        public ICollection<Address> Addresses { get; set; }
    }
}