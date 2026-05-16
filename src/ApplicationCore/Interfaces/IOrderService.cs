using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        // 异步创建订单
        Task CreateOrderAsync(int basketId, Address shippingAddress);
    }
}
