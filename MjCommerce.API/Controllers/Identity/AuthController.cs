using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Managers.Identity.Interfaces;
using MjCommerce.Shared.Models.Identity;
using MjCommerce.Shared.ViewModels.Identity;
using System;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersManager _usersManager;

        public AuthController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                if(model == null)
                {
                    return BadRequest();
                }

                var (result, message) = await _usersManager.RegisterAsync(model);

                if(!result.Succeeded)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(Get), new { id = message }, message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Register User operation");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var user = await _usersManager.FindByEmailAsync(model.Email);

                if(user == null)
                {
                    return BadRequest($"No user registered with email : {model.Email}");
                }

                var validPassword = await _usersManager.CheckPasswordAsync(user, model.Password);

                if(!validPassword)
                {
                    return BadRequest("Invalid password");
                }

                var token = await _usersManager.LoginUserAsync(user);

                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in User Login operation");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<ActionResult<User>> Get(string id)
        {
            try
            {
                var user = await _usersManager.FindByIdAsync(id);

                if(user == null)
                {
                    return NotFound($"User with Id : {id} not found");
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}