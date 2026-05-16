namespace Web.ViewModels
{
    // 订单项的视图模型，包含订单项的详细信息
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount => 0;  // 订单项的折扣，这里暂时设为0，实际应用中可以根据需要计算
        public int Units { get; set; }  // 订单项的数量
        public string? PictureUrl { get; set; } 
    }
}
