using ApplicationCore.Entities.CatalogAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class CatalogItemNameSpecification:Specification<CatalogItem>
    {
        // 根据目录项名称查询目录项。
        public CatalogItemNameSpecification(string catalogItemName)
        {
            Query
                .Where(c => c.Name == catalogItemName);
        }
    }
}
