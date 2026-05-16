using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IAppLogger<BasketService> _logger;

        public BasketService(IRepository<Basket> basketRepository, IAppLogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public async Task<Basket> AddItemToBasket(string userName, int catalogItemId, decimal price, int quantity = 1)
        {
            var basketUser = new BasketWithItemsSpecification(userName);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketUser);
            if (basket == null)
            {
                basket = new Basket(userName);
                await _basketRepository.AddAsync(basket);
            }
            basket.AddItem(catalogItemId, price, quantity);
            await _basketRepository.UpdateAsync(basket);
            return basket;
        }

        public async Task DeleteBasketAsync(int baskeId)
        {
            var basket = await _basketRepository.GetByIdAsync(baskeId);
            Guard.Against.Null(basket, nameof(basket));
            await _basketRepository.DeleteAsync(basket);
        }

        public async Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
        {
            var basketIdSpec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketIdSpec);
            if (basket == null) return Result<Basket>.NotFound();

            foreach (var item in basket.Items)
            {
                if(quantities.TryGetValue(item.Id.ToString(),out int quantity))
                {
                    if (_logger != null)
                    {
                        _logger.LogInformation($"将商品ID为{item.Id}的数量更新为{quantity}");
                    }
                    item.SetQuantity(quantity);
                }
            }
            basket.RemoveEmptyItems();
            await _basketRepository.UpdateAsync(basket);
            return Result<Basket>.Success(basket);
        }

        public async Task TransferBasketAsync(string anonymousId, string userName)
        {
            var anonymousBasketSpec = new BasketWithItemsSpecification(anonymousId);
            var anonymousBasket = await _basketRepository.FirstOrDefaultAsync(anonymousBasketSpec);
            if (anonymousBasket == null) return;

            var userBasketSpec=new BasketWithItemsSpecification(userName);
            var userBasket = await _basketRepository.FirstOrDefaultAsync(userBasketSpec);
            if(userBasket == null)
            {
                userBasket = new Basket(userName);
                await _basketRepository.AddAsync(userBasket);
            }
            // 遍历循环将匿名选中的所有商品加入到该用户的购物车中
            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item.CatalogItemId, item.UnitPrice, item.Quantity);
            }
            await _basketRepository.UpdateAsync(userBasket);
            await _basketRepository.DeleteAsync(anonymousBasket);
        }
    }
}
