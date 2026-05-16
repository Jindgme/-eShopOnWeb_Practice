using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.BasketAggregate
{
    /// <summary>
    /// 购物车项 实体
    /// </summary>
    public class BasketItem:BaseEntity
    {
        public decimal UnitPrice { get;private set; } // 单价
        public int Quantity { get; private set; }  //商品数量
        public int BasketId { get; private set; } 
        public int CatalogItemId { get; private set; } // 商品目录项Id

        public BasketItem(int catalogItemId,int quantity,decimal unitPrice)
        {
            CatalogItemId = catalogItemId;
            SetQuantity(quantity);
            UnitPrice = unitPrice;
        }
        public void AddQuantity(int quantity)
        {
            // 数据验证，数量不能小于0
            Guard.Against.OutOfRange(quantity,nameof(quantity),0,int.MaxValue);
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);
            Quantity = quantity;
        }
    }
}
