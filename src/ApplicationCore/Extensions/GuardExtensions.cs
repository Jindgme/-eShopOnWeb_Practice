using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Exceptions;
using Ardalis.GuardClauses;

namespace ApplicationCore.Extensions
{
    public static class GuardExtensions
    {
        /// <summary>
        /// 业务检查
        /// 结账时购物车不能为空
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="Items"></param>
        public static void EmptyBasketOnCheckout(this IGuardClause guardClause,
            IEnumerable<BasketItem> Items)
        {
            if (!Items.Any())
            {
                throw new EmptyBasketOnCheckoutException();
            }
        }
    }
}
