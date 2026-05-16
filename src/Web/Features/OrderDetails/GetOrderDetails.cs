using MediatR;
using Web.ViewModels;

namespace Web.Features.OrderDetails
{
    public class GetOrderDetails:IRequest<OrderDetailViewModel>
    {
        public GetOrderDetails(int orderId)
        {
            OrderId = orderId;
        }

        //public string UserName { get; set; }
        public int OrderId { get; set; }
    }
}
