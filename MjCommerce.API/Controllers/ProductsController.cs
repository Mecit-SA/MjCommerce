using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;
using MjCommerce.Shared.Services.Identity.Interfaces;
using System;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Seller))]
    public class ProductsController : CrudController<Product, ProductFilter>
    {
        private readonly IRepository<Product> _repository;
        private readonly IProductOwnerAuthorizationService _productOwnerAuthorizationService;

        public ProductsController(IRepository<Product> repository,
            IProductOwnerAuthorizationService productOwnerAuthorizationService) : base(repository)
        {
            _repository = repository;
            _productOwnerAuthorizationService = productOwnerAuthorizationService;
        }

        public async override Task<ActionResult<Product>> Update(int id, Product entity)
        {
            try
            {
                if (User.IsInRole(nameof(Roles.Seller)))
                {
                    if (!(await _productOwnerAuthorizationService.AuthorizeAsync(User, entity)).Succeeded)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden);
                    }
                }

                return Ok(await base.Update(id, entity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating product");
            }
            
        }

        public async override Task<ActionResult<int>> Delete(int id)
        {
            try
            {
                var productToDelete = await _repository.Get(id);

                if (productToDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                if (User.IsInRole(nameof(Roles.Seller)))
                {
                    if (!(await _productOwnerAuthorizationService.AuthorizeAsync(User, productToDelete)).Succeeded)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden);
                    }
                }

                return Ok(await _repository.Delete(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting product");
            }
        }
    }
}