using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.Shared.Filters;
using MjCommerce.Shared.Helpers.Identity;
using MjCommerce.Shared.Managers.Files.Interfaces;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Interfaces;
using MjCommerce.Shared.Services.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MjCommerce.API.Controllers.Files
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Seller))]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesManager _imagesManager;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductPhoto> _productPhotoRepository;
        private readonly IProductOwnerAuthorizationService _productOwnerAuthorizationService;

        public ImagesController(IImagesManager imagesManager, 
            IRepository<Product> productRepository,
            IRepository<ProductPhoto> productPhotoRepository,
            IProductOwnerAuthorizationService productOwnerAuthorizationService)
        {
            _imagesManager = imagesManager;
            _productRepository = productRepository;
            _productPhotoRepository = productPhotoRepository;
            _productOwnerAuthorizationService = productOwnerAuthorizationService;
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public ActionResult Download(string name)
        {
            try
            {
                var result = _imagesManager.Get(name);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error downloading image");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> Upload([FromForm] IEnumerable<IFormFile> files)
        {
            try
            {
                if (files == null || !files.Any())
                {
                    return BadRequest();
                }

                var names = await _imagesManager.UploadAsync(files);
                return CreatedAtAction(nameof(Download), new { name = names.First() }, names);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error uploading image(s)");
            }
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<string>> Delete(string name)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest();
                }

                // Get product which photo is related to
                var products = await _productRepository.Get(new ProductFilter()
                {
                    PhotoName = name
                });

                if(!products.Any())
                {
                    return BadRequest("Photo is not related to any product");
                }

                var product = products.First();

                // Check if seller AND owner
                if (User.IsInRole(nameof(Roles.Seller)))
                {
                    if (!(await _productOwnerAuthorizationService.AuthorizeAsync(User, product)).Succeeded)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden);
                    }
                }

                // Delete from server
                var result = await _imagesManager.DeleteAsync(name);

                if (string.IsNullOrEmpty(result))
                {
                    return NotFound("File not found");
                }


                // Delete from table
                var photos = await _productPhotoRepository.Get(new ProductPhotoFilter
                {
                    Name = name
                });

                if(photos == null || !photos.Any())
                {
                    return NotFound("Record not found");
                }

                await _productPhotoRepository.Delete(photos.First().Id);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting image");
            }
        }
    }
}