using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class CategoryFilter : PagingFilter<Category>, IFilter<Category>
    {
        public int ParentId { get; set; }
        public string Name { get; set; }

        public IQueryable<Category> Build(IQueryable<Category> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(c => c.Active == Active);
            }

            if (ParentId > 0)
            {
                initialSet = initialSet.Where(c => c.ParentId == ParentId);
            }

            if(!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(c => c.Name.Contains(Name));
            }

            if (applyPaging)
            {
                return Build(initialSet);
            }

            return initialSet;
        }
    }
}