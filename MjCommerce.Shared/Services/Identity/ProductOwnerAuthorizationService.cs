using Microsoft.AspNetCore.Authorization;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Services.Identity.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MjCommerce.Shared.Services.Identity
{
    public class ProductOwnerAuthorizationService : IProductOwnerAuthorizationService
    {
        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, Product product)
        {
            if (product.SellerId == user.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Task.FromResult(AuthorizationResult.Success());
            }

            return Task.FromResult(AuthorizationResult.Failed());
        }
    }
}