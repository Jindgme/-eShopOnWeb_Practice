namespace ApplicationCore.Interfaces
{
    // 用于不占用内存的情况下获取特定查询
    public interface IBasketQueryService
    {
        Task<int> CountTotalBasketItems(string userName);
    }
}
