using System.Collections.Generic;
using System.Reflection;

namespace MjCommerce.Shared.Filters.Base
{
    public abstract class FilterBase
    {
        public bool Active { get; set; } = true;

        public string ToQueryString(PropertyInfo[] properties)
        {
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