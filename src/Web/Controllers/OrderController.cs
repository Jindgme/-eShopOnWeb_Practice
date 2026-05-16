using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Features.MyOrders;
using Web.Features.OrderDetails;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var viewModel = await _mediator.Send(new GetMyOrders("小马"));
            return View(viewModel);
        }
        [HttpGet("orderId")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var viewModel = await _mediator.Send(new GetOrderDetails(orderId));
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
    }
}
