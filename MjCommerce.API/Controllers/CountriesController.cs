﻿using Microsoft.AspNetCore.Authorization;
using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;

namespace MjCommerce.API.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CountriesController : CrudController<Country, CountryFilter>
    {
        public CountriesController(IRepository<Country> repository) : base(repository)
        {

        }
    }
}