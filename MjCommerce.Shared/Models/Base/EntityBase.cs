namespace MjCommerce.Shared.Models.Base
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
    }
}