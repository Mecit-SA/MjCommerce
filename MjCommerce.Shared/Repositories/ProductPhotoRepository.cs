using AutoMapper;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Base;

namespace MjCommerce.Shared.Repositories
{
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>
    {
        public ProductPhotoRepository(MjCommerceDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}