using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Basket
{
    public class CheckoutModel : PageModel
    {
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly IAppLogger<CheckoutModel> _appLogger;

        public CheckoutModel(IBasketViewModelService basketViewModelService, IBasketService basketService, IOrderService orderService, IAppLogger<CheckoutModel> appLogger)
        {
            _basketViewModelService = basketViewModelService;
            _basketService = basketService;
            _orderService = orderService;
            _appLogger = appLogger;
        }

        public BasketViewModel BasketModel { get; set; }=new BasketViewModel();
        public async Task OnGet()
        {
            BasketModel = await _basketViewModelService.GetOrCreateBasketForUser("小马");
        }
        public async Task<IActionResult> OnPost(IEnumerable<BasketItemViewModel> items)
        {
            try
            {
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser("小马");
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                // 下面两行在这里没什么用，因为在支付确认页面数量不会变动了。参考实际应用，需要改数量这里应该加上。
                //var updateModel = items.ToDictionary(i => i.Id.ToString(), c => c.Quantity);
                //await _basketService.SetQuantities(BasketModel.Id, updateModel);

                var address = new Address("中国", "浙江", "杭州", "西湖", "123456");
                await _orderService.CreateOrderAsync(BasketModel.Id, address);
                await _basketService.DeleteBasketAsync(BasketModel.Id);
            }
            catch (EmptyBasketOnCheckoutException emptyBasketOnCheckoutException)
            {
                _appLogger.LogWarning(emptyBasketOnCheckoutException.Message);
                return RedirectToPage("/Basket/Index");
            }
           
            return RedirectToPage("/Basket/Success");
        }
    }
}
