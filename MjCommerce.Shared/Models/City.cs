﻿using MjCommerce.Shared.Attributes;
using MjCommerce.Shared.Models.Base;
using MjCommerce.Shared.Models.Identity;
using System.Collections.Generic;

namespace MjCommerce.Shared.Models
{
    public class City : EntityBase
    {
        [MjRequired]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}