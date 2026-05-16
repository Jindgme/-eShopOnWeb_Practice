using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Queries
{
    public class BasketQueryService : IBasketQueryService
    {
        private readonly CatalogDbContext _context;

        public BasketQueryService(CatalogDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 这个方法是在数据库上求和操作而不再内存上。
        /// 这里统计的是商品种类数量。官方源码统计的是 单个商品*数量+单个商品*数量+单个商品*数量.........................
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<int> CountTotalBasketItems(string userName)
        {
            var totalItems = await _context.Baskets
                .Where(basket => basket.BuyerId == userName)
                .SelectMany(item => item.Items)
                .CountAsync();
                //.SumAsync(sum => sum.Quantity);
            return totalItems;
        }
    }
}
