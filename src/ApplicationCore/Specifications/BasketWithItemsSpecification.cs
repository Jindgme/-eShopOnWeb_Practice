using ApplicationCore.Entities.BasketAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    // 购物车规约。根据购物Id查询或者根据用户查询
    public class BasketWithItemsSpecification : Specification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            Query
                .Where(b => b.Id == basketId)
                .Include(b => b.Items);
        }
        public BasketWithItemsSpecification(string buyerId)
        {
            Query
                .Where(b => b.BuyerId == buyerId)
                .Include(b => b.Items);
        }
    }
}
