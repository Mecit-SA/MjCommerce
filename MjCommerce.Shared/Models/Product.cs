﻿using MjCommerce.Shared.Attributes;
using MjCommerce.Shared.Models.Base;
using System.Collections.Generic;

namespace MjCommerce.Shared.Models
{
    public class Product : EntityBase
    {
        [MjRequired]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public decimal OriginalPrice { get; set; }
        public float DiscountPercentage { get; set; }
        public decimal Price {
            get => DiscountPercentage <= 0 ? 
                   OriginalPrice :
                   OriginalPrice - (OriginalPrice * (decimal)DiscountPercentage / 100);
        }

        public string CoverPhotoName { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; }
    }
}