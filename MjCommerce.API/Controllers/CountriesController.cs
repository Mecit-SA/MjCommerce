using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;

namespace MjCommerce.API.Controllers
{
    public class CountriesController : CrudController<Country, CountryFilter>
    {
        public CountriesController(IRepository<Country> repository) : base(repository)
        {

        }
    }
}