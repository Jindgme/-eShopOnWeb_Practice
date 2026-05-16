using ApplicationCore.Entities.OrderAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class CustomerOrdersSpecification:Specification<Order>
    {
        public CustomerOrdersSpecification(string buyerId)
        {
            Query.Where(o => o.BuyerId == buyerId)
                .Include(o => o.OrderItems);
        }
    }
}
