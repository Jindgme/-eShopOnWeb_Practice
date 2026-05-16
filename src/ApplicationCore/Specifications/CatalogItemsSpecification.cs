using ApplicationCore.Entities.CatalogAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class CatalogItemsSpecification:Specification<CatalogItem>
    {
        // 检查CatalogItem中是否有与指定集合（ids）相等的元素
        // 如果c.Id属于 ids集合时，该商品都会被查询出来
        public CatalogItemsSpecification(params int[] ids)
        {
            Query
                .Where(c => ids.Contains(c.Id));
        }
    }
}
