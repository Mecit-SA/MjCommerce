using MjCommerce.Shared.Attributes;
using MjCommerce.Shared.Models.Base;
using System.Collections.Generic;

namespace MjCommerce.Shared.Models
{
    public class Country : EntityBase
    {
        [MjRequired]
        [MjStringLength(1,10)]
        public string PhoneCode { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}