using MjCommerce.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MjCommerce.Shared.ViewModels.Identity
{
    public class LoginViewModel
    {
        [MjRequired]
        [MjStringLength(3, 50)]
        [EmailAddress]
        public string Email { get; set; }

        [MjRequired]
        [MjStringLength(6, 50)]
        public string Password { get; set; }
    }
}