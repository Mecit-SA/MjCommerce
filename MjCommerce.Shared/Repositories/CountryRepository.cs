using AutoMapper;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Base;

namespace MjCommerce.Shared.Repositories
{
    public class CountryRepository : RepositoryBase<Country>
    {
        public CountryRepository(MjCommerceDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
    }
}