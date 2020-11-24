using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers
{
    public class CountriesController : CrudController<Country, CountryFilter>
    {
        public CountriesController(IRepository<Country> repository) : base(repository)
        {

        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public override Task<ActionResult<int>> Add(Country entity)
        {
            return base.Add(entity);
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public override Task<ActionResult<Country>> Update(int id, Country entity)
        {
            return base.Update(id, entity);
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public override Task<ActionResult<int>> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}