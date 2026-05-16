using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.ViewComponents
{
    // 默认购物车视图组件类
    public class BasketViewComponent:ViewComponent
    {
        private IBasketViewModelService _viewModelService;

        public BasketViewComponent(IBasketViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var itemsCount =await _viewModelService.CountTotalBasketItems("小马");
            var vm = new BasketComponentViewModel
            {
                ItemsCount = itemsCount
            };
            return View(vm);
        }
    }
}
