using ApplicationCore.Entities.OrderAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class OrderWithItemsByIdSpecification:Specification<Order>
    {
        public OrderWithItemsByIdSpecification(int orderId)
        {
            Query
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi=>oi.ItemOrdered);
        }
    }
}
