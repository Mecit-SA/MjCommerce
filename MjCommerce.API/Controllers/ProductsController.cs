using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;

namespace MjCommerce.API.Controllers
{
    public class ProductsController : CrudController<Product, ProductFilter>
    {
        public ProductsController(IRepository<Product> repository) : base(repository)
        {

        }
    }
}