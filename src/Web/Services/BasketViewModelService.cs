using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IRepository<CatalogItem> _catalogItemRepository;
        private readonly IBasketQueryService _basketQueryService;

        public BasketViewModelService(IRepository<Basket> basketRepository, IUriComposer uriComposer, IRepository<CatalogItem> catalogItemRepository, IBasketQueryService basketQueryService)
        {
            _basketRepository = basketRepository;
            _uriComposer = uriComposer;
            _catalogItemRepository = catalogItemRepository;
            _basketQueryService = basketQueryService;
        }

        public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
        {
            var userBasketSpec = new BasketWithItemsSpecification(userName);
            var userBasket = await _basketRepository.FirstOrDefaultAsync(userBasketSpec);
            if (userBasket == null)
            {
                // 重新创建购物车。不需要用到Map方法返回，因为现在购物车中没有商品。
                var basket = new Basket(userName);
                await _basketRepository.AddAsync(basket);
                return new BasketViewModel
                {
                    BuyerId = basket.BuyerId,
                    Id = basket.Id
                };
            }
            return await Map(userBasket);
        }
        public async Task<int> CountTotalBasketItems(string userName)
        {
            return await _basketQueryService.CountTotalBasketItems(userName);
        }
        
        public async Task<BasketViewModel> Map(Basket basket)
        {
            return new BasketViewModel
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items =await GetBasketItems(basket.Items)
            };
        }
        // 将领域实体BasketItem 转换成BasketItemViewModel视图模型
        private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
        {
            var catalogItemsSpecification=new CatalogItemsSpecification(basketItems.Select(b=>b.CatalogItemId).ToArray());
            var catalogItems = await _catalogItemRepository.ListAsync(catalogItemsSpecification);

            var items = basketItems.Select(basketItem =>
            {
                var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);

                var basketItemViewModel = new BasketItemViewModel
                {
                    Id = basketItem.Id,
                    UnitPrice = basketItem.UnitPrice,
                    Quantity = basketItem.Quantity,
                    CatalogItemId = basketItem.CatalogItemId,
                    ProductName = catalogItem.Name,
                    PictureUrl = _uriComposer.ComposePictureUri(catalogItem.PictureUri)
                };
                return basketItemViewModel;
            }).ToList();
            return items;
        }
    }
}
