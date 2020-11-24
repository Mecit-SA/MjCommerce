using MjCommerce.Shared.Filters.Base;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class CityFilter : PagingFilter<City>, IFilter<City>
    {
        public int? CategoryId { get; set; }

        public string Name { get; set; }

        public IQueryable<City> Build(IQueryable<City> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(c => c.Active == Active);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(c => c.Name.Contains(Name));
            }

            if (CategoryId.HasValue && CategoryId.Value > 0)
            {
                initialSet = initialSet.Where(c => c.CountryId == CategoryId.Value);
            }

            if (applyPaging)
            {
                return Build(initialSet);
            }

            return initialSet;
        }

        public override string ToString()
        {
            return ToQueryString(GetType().GetProperties());
        }
    }
}