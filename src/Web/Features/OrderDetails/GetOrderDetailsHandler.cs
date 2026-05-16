using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using MediatR;
using Web.ViewModels;

namespace Web.Features.OrderDetails
{
    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetails, OrderDetailViewModel?>
    {
        private readonly IRepository<Order> _orderRepository;

        public GetOrderDetailsHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetailViewModel?> Handle(GetOrderDetails request, CancellationToken cancellationToken)
        {
            var orderSepc = new OrderWithItemsByIdSpecification(request.OrderId);
            var order = await _orderRepository.FirstOrDefaultAsync(orderSepc, cancellationToken);
            if (order == null)
            {
                return null;
            }
            return new OrderDetailViewModel
            {
                OrderNumber=order.Id,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShipToAddress,
                TotalPrice = order.Total(),
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList()
            };
        }
    }
}
