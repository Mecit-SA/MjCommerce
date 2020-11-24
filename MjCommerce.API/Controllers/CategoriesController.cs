using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.API.Controllers.Base;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CategoriesController : CrudController<Category, CategoryFilter>
    {
        private readonly IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository) : base(repository)
        {
            _repository = repository;
        }


        public async override Task<ActionResult<int>> Add(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                if (await _repository.Conatins(category.Name))
                {
                    return BadRequest("Category name already exists");
                }

                var createdEntityId = await _repository.Add(category);

                return CreatedAtAction(nameof(Get), new { id = createdEntityId }, createdEntityId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding data to the database");
            }
        }
    }
}