using MjCommerce.Shared.Attributes;

namespace MjCommerce.Shared.Models.Identity
{
    public class Address
    {
        public int Id { get; set; }

        [MjRequired]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        [MjRequired]
        public int CityId { get; set; }
        public City City { get; set; }

        [MjRequired]
        [MjStringLength(5,100)]
        public string AddressDetail { get; set; }
    }
}