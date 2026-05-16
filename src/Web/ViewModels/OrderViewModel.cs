using ApplicationCore.Entities.OrderAggregate;

namespace Web.ViewModels
{
    public class OrderViewModel
    {
        private const string DEAFAULT_STATUS = "待定";
        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = DEAFAULT_STATUS;
        public Address? ShippingAddress { get; set; }

    }
}
