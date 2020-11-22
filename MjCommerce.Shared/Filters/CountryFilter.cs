using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class CountryFilter : PagingFilter<Country>, IFilter<Country>
    {
        public string Name { get; set; }
        public string PhoneCode { get; set; }

        public IQueryable<Country> Build(IQueryable<Country> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(c => c.Active == Active);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(c => c.Name.Contains(Name));
            }

            if (!string.IsNullOrWhiteSpace(PhoneCode))
            {
                initialSet = initialSet.Where(c => c.PhoneCode.Contains(PhoneCode));
            }

            if (applyPaging)
            {
                return Build(initialSet);
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