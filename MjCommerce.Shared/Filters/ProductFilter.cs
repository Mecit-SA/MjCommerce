using MjCommerce.Shared.Filters.Base;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class ProductFilter : PagingFilter<Product>, IFilter<Product>
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string PhotoName { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }

        public IQueryable<Product> Build(IQueryable<Product> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(p => p.Active == Active);
            }

            if(CategoryId.HasValue && CategoryId.Value > 0)
            {
                initialSet = initialSet.Where(p => p.CategoryId == CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(p => p.Name.Contains(Name));
            }

            if (!string.IsNullOrWhiteSpace(PhotoName))
            {
                initialSet = initialSet.Where(p => p.Photos.Where(p => p.Name.Equals(PhotoName)).Any());
            }

            if (MinPrice.HasValue && MinPrice.Value > 0)
            {
                initialSet = initialSet.Where(p => p.Price >= (decimal)MinPrice.Value);
            }

            if(MaxPrice.HasValue && MaxPrice.Value > 0)
            {
                initialSet = initialSet.Where(p => p.Price <= (decimal)MaxPrice.Value);
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