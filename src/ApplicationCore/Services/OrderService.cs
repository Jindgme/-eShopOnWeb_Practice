using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<CatalogItem> _catalogRepository;
        private readonly IUriComposer _uriComposer;
        public OrderService(IRepository<Order> repository, IRepository<Basket> basketRepository, IRepository<CatalogItem> catalogRepository, IUriComposer uriComposer)
        {
            _orderRepository = repository;
            _basketRepository = basketRepository;
            _catalogRepository = catalogRepository;
            _uriComposer = uriComposer;
        }
        /// <summary>
        /// 1.根据购物车Id获取购物车信息
        /// 2.再根据获取的购物车的商品吗目录Id,查询出所有的商品
        /// 3.根据获取的所有商品项，将购物车项列表转换成订单项
        /// 4.最后生成订单添加到数据库里
        /// </summary>
        /// <param name="basketId"></param>
        /// <param name="shippingAddress"></param>
        /// <returns></returns>
        public async Task CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var basketSpec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            Guard.Against.Null(basket, nameof(basket));  // 空异常验证数据库层购物车是否存在
            Guard.Against.EmptyBasketOnCheckout(basket.Items);  // 业务层验证购物车商品项不能为空

            var catalogItemsSepc = new CatalogItemsSpecification(basket.Items.Select(c => c.CatalogItemId).ToArray());
            var catalogItems = await _catalogRepository.ListAsync(catalogItemsSepc);

            var items = basket.Items.Select(basketItem =>
            {
                var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);
                var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, _uriComposer.ComposePictureUri(catalogItem.PictureUri));
                var orderItem = new OrderItem(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
                return orderItem;
            }).ToList();

            var order = new Order(basket.BuyerId, shippingAddress, items);
            await _orderRepository.AddAsync(order);
        }
    }
}
