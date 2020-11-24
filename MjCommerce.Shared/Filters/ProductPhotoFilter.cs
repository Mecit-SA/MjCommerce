using MjCommerce.Shared.Filters.Base;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class ProductPhotoFilter : PagingFilter<ProductPhoto>, IFilter<ProductPhoto>
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }

        public IQueryable<ProductPhoto> Build(IQueryable<ProductPhoto> initialSet, bool applyPaging)
        {
            if(Active)
            {
                initialSet = initialSet.Where(p => p.Active == Active);
            }

            if(!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(p => p.Name.Equals(Name));
            }

            if(ProductId.HasValue && ProductId.Value > 0)
            {
                initialSet = initialSet.Where(p => p.ProductId == ProductId.Value);
            }

            if(applyPaging)
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