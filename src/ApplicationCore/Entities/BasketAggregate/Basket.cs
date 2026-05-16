using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.BasketAggregate
{
    /// <summary>
    ///  购物车实体
    /// </summary>
    public class Basket:BaseEntity,IAggregateRoot
    {
        public string BuyerId { get;private set; }
        private readonly List<BasketItem> _items=new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items=>_items.AsReadOnly();
        // 购物车商品总数量
        public int TotalItems => _items.Sum(i => i.Quantity);
        public Basket(string buyerId)
        {
            BuyerId = buyerId;  // 初始化用户Id
        }
        // 如果购物车没有该商品则新增
        // 如果有则添加
        public void AddItem(int catalogItemId,decimal unitPrice, int quantity = 1)
        {
            if(!Items.Any(i=>i.CatalogItemId == catalogItemId))
            {
                _items.Add(new BasketItem(catalogItemId, quantity, unitPrice));
                return;
            }
            var existingItem=Items.First(i=>i.CatalogItemId == catalogItemId);
            existingItem.AddQuantity(quantity);
        }
        // 移除购物车该商品
        public void RemoveEmptyItems()
        {
            _items.RemoveAll(x=>x.Quantity == 0);
        }
        // 允许修改购物者Id
        public void SetNewBuyerId(string buyerId)
        {
            BuyerId=buyerId;  
        }
    }
}
