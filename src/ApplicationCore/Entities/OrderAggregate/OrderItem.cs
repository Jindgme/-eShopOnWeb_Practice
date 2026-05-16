namespace ApplicationCore.Entities.OrderAggregate
{
    //订单项 实体
    public class OrderItem:BaseEntity
    {
        public OrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }
        private OrderItem() { }
        public CatalogItemOrdered ItemOrdered { get;private set; }
        public decimal UnitPrice { get; private set; }  // 单价快照，从商品目录复制而来
        public int Units { get; private set; }  // 购物车中该商品的数量
    }
}
