using MjCommerce.Shared.Models.Base;
using System.Collections.Generic;

namespace MjCommerce.Shared.Models
{
    public class Category : EntityBase
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }

        public string CoverPhotoName { get; set; }

        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}