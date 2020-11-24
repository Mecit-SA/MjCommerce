using AutoMapper;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Repositories.Base;

namespace MjCommerce.Shared.Repositories
{
    public class CityRepository : RepositoryBase<City>
    {
        public CityRepository(MjCommerceDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
    }
}