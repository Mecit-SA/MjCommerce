using MjCommerce.Shared.Models.Base;

namespace MjCommerce.Shared.Models
{
    public class ProductPhoto : PhotoBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}