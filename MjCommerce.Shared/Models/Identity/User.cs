using Microsoft.AspNetCore.Identity;
using MjCommerce.Shared.Attributes;

namespace MjCommerce.Shared.Models.Identity
{
    public class User : IdentityUser
    {
        [MjRequired]
        [MjStringLength(2,100)]
        public string FullName { get; set; }
    }
}