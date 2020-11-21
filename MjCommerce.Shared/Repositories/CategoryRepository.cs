using AutoMapper;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Base;

namespace MjCommerce.Shared.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(MjCommerceDbContext context, IMapper mapper) 
            : base(context, mapper)
        {

        }
    }
}