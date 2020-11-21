using AutoMapper;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Base;

namespace MjCommerce.Shared.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(MjCommerceDbContext context, IMapper mapper) 
            : base(context, mapper)
        {

        }
    }
}