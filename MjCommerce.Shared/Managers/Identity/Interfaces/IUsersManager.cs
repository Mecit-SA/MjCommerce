using Microsoft.AspNetCore.Identity;
using MjCommerce.Shared.Models.Identity;
using MjCommerce.Shared.ViewModels.Identity;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Managers.Identity.Interfaces
{
    public interface IUsersManager
    {
        Task<(IdentityResult, string)> RegisterAsync(RegisterViewModel model);
        Task<string> LoginUserAsync(User user);
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<User> FindByIdAsync(string id);
    }
}