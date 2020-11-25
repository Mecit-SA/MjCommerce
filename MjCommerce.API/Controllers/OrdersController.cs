using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Models.Orders;
using MjCommerce.Shared.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class OrdersController : CrudController<Order, OrderFilter>
    {
        public OrdersController(IRepository<Order> repository) : base(repository)
        {

        }

        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Customer))]
        public override Task<ActionResult<int>> Add(Order entity)
        {
            return base.Add(entity);
        }
    }
}