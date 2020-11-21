using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class ProductFilter : PagingFilter<Product>, IFilter<Product>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

        public IQueryable<Product> Build(IQueryable<Product> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(p => p.Active == Active);
            }

            if(CategoryId > 0)
            {
                initialSet = initialSet.Where(p => p.CategoryId == CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(p => p.Name.Contains(Name));
            }

            if(MinPrice > 0)
            {
                initialSet = initialSet.Where(p => p.Price >= (decimal)MinPrice);
            }

            if(MaxPrice > 0)
            {
                initialSet = initialSet.Where(p => p.Price <= (decimal)MaxPrice);
            }

            return initialSet;
        }

        public override string ToString()
        {
            var properties = GetType().GetProperties();

            ICollection<string> parameters = new List<string>();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(this);

                if (propertyValue != null)
                {
                    parameters.Add($"{property.Name}={propertyValue}");
                }
            }

            return string.Join("&", parameters);
        }
    }
}