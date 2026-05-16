using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.OrderAggregate
{
    //订单实体
    public class Order:BaseEntity,IAggregateRoot
    {
        private Order(){ }
        public Order(string buyerId,Address shipToAddress, List<OrderItem> orderItems)
        {
            Guard.Against.NullOrEmpty(buyerId,nameof(buyerId));
            BuyerId = buyerId;
            ShipToAddress = shipToAddress;
            _orderItems = orderItems;
        }
        public string BuyerId { get;private set; }
        public DateTimeOffset OrderDate { get; private set; } =DateTimeOffset.Now;  // 订单日期
        public Address ShipToAddress { get; private set; }  // 收获地址

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        // 订单总价
        public decimal Total()
        {
            var total = 0m;
            foreach (var item in _orderItems)
            {
                total += item.UnitPrice * item.Units;
            }
            return total;
        }
    }
}
