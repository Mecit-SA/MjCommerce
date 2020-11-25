using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Base;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Repositories
{
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>
    {
        private readonly MjCommerceDbContext _context;

        public ProductPhotoRepository(MjCommerceDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public async Task<string> Delete(string name)
        {
            var result = await _context.Photos.FirstOrDefaultAsync(e => e.Name.Equals(name));

            if (result != null)
            {
                _context.Photos.Remove((ProductPhoto)result);
                await _context.SaveChangesAsync();
            }

            return result.Name;
        }
    }
}