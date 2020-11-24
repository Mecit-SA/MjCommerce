using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers
{
    public class ProductsController : CrudController<Product, ProductFilter>
    {
        public ProductsController(IRepository<Product> repository) : base(repository)
        {

        }

        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Seller))]
        public override Task<ActionResult<int>> Add(Product entity)
        {
            return base.Add(entity);
        }

        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Seller))]
        public async override Task<ActionResult<Product>> Update(int id, Product entity)
        {
            if(User.IsInRole(nameof(Roles.Seller)) &&
                entity.SellerId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return StatusCode(403);
            }

            return await base.Update(id, entity);
        }

        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Seller))]
        public override Task<ActionResult<int>> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}