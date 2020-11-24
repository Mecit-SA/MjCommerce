using MjCommerce.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MjCommerce.Shared.ViewModels.Identity
{
    public class RegisterViewModel : LoginViewModel
    {
        [MjRequired]
        [MjStringLength(2, 100)]
        public string FullName { get; set; }

        [MjRequired]
        [MjStringLength(6, 50)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}