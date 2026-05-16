using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly IRepository<CatalogItem> _itemRepository;
        public IndexModel(IBasketService basketService, IBasketViewModelService basketViewModelService, IRepository<CatalogItem> itemRepository)
        {
            _basketService = basketService;
            _basketViewModelService = basketViewModelService;
            _itemRepository = itemRepository;
        }
        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();
        public List<BasketItemViewModel> Items { get; set; }=new List<BasketItemViewModel>();
        public async Task OnGet()
        {
            BasketModel = await _basketViewModelService.GetOrCreateBasketForUser("小马");
        }
        public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            var item=await _itemRepository.GetByIdAsync(productDetails.Id);
            if (item == null)
            {
                return RedirectToPage("/Index");
            }
            var basket = await _basketService.AddItemToBasket("小马",
                productDetails.Id, item.Price);
            BasketModel = await _basketViewModelService.Map(basket);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdate(IEnumerable<BasketItemViewModel> items)
        {
            if (!ModelState.IsValid)
            {
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser("小马");
                return Page();
            }
            var basketView = await _basketViewModelService.GetOrCreateBasketForUser("小马");
            var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
            //  ********** 隐式转换 Rseult<T> ---> T **********
            var basket = await _basketService.SetQuantities(basketView.Id, updateModel);
            BasketModel = await _basketViewModelService.Map(basket.Value);
            return RedirectToPage();
        }
    }
}
