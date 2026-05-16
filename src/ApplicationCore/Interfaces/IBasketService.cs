using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Extensions;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        /// <summary>
        /// 将匿名用户的购物车转移到已登录用户
        /// </summary>
        /// <param name="anonymousId">匿名用户标识</param>
        /// <param name="userName">登录用户名</param>
        /// <returns></returns>
        Task TransferBasketAsync(string anonymousId, string userName);
        /// <summary>
        /// 向指定用户的购物车中添加一个商品项
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="catalogItemId">商品目录Id</param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<Basket> AddItemToBasket(string userName, int catalogItemId, decimal price, int quantity = 1);
        /// <summary>
        /// 批量设置购物车中各个商品的数量
        /// </summary>
        /// <param name="basketId">购物车Id</param>
        /// <param name="quantities"></param>
        /// <returns></returns>
        Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
        /// <summary>
        /// 删除指定购物车
        /// </summary>
        /// <param name="baskeId">购物车ID</param>
        /// <returns></returns>
        Task DeleteBasketAsync(int baskeId);
    }
}
