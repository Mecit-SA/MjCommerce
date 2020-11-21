using Microsoft.AspNetCore.Identity;

namespace MjCommerce.Shared.Models
{
    class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
