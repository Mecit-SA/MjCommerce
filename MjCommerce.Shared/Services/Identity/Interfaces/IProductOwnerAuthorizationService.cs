using Microsoft.AspNetCore.Authorization;
using MjCommerce.Shared.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Services.Identity.Interfaces
{
    public interface IProductOwnerAuthorizationService
    {
        Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, Product product);
    }
}