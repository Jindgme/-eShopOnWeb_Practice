using ApplicationCore.Entities.BasketAggregate;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        // 获取指定用户的购物车。用于前端渲染
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
        // 统计用户购物车所有商品的总数量
        Task<int> CountTotalBasketItems(string userName);
        // 将领域实体Basket转换成视图模型BasketViewModel
        Task<BasketViewModel> Map(Basket basket);
    }
}
