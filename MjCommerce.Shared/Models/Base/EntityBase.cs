using MjCommerce.Shared.Attributes;

namespace MjCommerce.Shared.Models.Base
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        [MjRequired]
        [MjStringLength(2, 100)]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}