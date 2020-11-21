using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models.Base;
using MjCommerce.Shared.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CrudController<T, FilterType> : ControllerBase
        where T : EntityBase, new()
        where FilterType : IFilter<T>
    {
        private readonly IRepository<T> _repository;

        public CrudController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            try
            {
                return Ok(await _repository.Get());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retriving data from the database");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<T>>> Get([FromQuery] FilterType filter)
        {
            try
            {
                return Ok(await _repository.Get(filter));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retriving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<T>> Get(int id)
        {
            try
            {
                var result = await _repository.Get(id);

                if (result == null)
                {
                    return NotFound($"Entity with Id = {id} not found");
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retriving data from the database");
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<int>> Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest();
                }

                var createdEntityId = await _repository.Add(entity);

                return CreatedAtAction(nameof(Get), new { id = createdEntityId }, createdEntityId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<T>> Update(int id, T entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return BadRequest("Entity ID mismatch");
                }

                var entityToUpdate = await _repository.Get(id);

                if (entityToUpdate == null)
                {
                    return NotFound($"Entity with Id = {id} not found");
                }

                return await _repository.Update(entity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            try
            {
                var entityToDelete = await _repository.Get(id);

                if (entityToDelete == null)
                {
                    return NotFound($"Entity with Id = {id} not found");
                }

                return await _repository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}