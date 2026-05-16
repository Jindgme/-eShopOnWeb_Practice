using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using MediatR;
using Web.ViewModels;

namespace Web.Features.MyOrders
{
    public class GetMyOrdersHandler : IRequestHandler<GetMyOrders, IEnumerable<OrderViewModel>>
    {
        private readonly IRepository<Order> _orderRepository;

        public GetMyOrdersHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        // 根据用户名获取订单列表
        public async Task<IEnumerable<OrderViewModel>> Handle(GetMyOrders request, CancellationToken cancellationToken)
        {
            var ordersSpecification = new CustomerOrdersSpecification(request.UserName);
            var orders=await _orderRepository.ListAsync(ordersSpecification);

            return orders.Select(o => new OrderViewModel
            {
                OrderNumber = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.Total(),
                ShippingAddress = o.ShipToAddress
            });
        }
    }
}
