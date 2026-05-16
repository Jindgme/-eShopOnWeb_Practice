using ApplicationCore.Entities.CatalogAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    // 按照商品品牌、类型过滤规约
    public class CatalogFilterSpecification:Specification<CatalogItem>
    {
        /// <summary>
        /// 如果brandId、typeId没有值，则表示不进行品牌和类型过滤
        /// 如果brandId、typeId都有值，或者其中一个有值。则进行该值过滤，并比较ID是否相等。没有值的不受影响
        /// </summary>
        public CatalogFilterSpecification(int? brandId,int? typeId)
        {
            Query.Where(c => (!brandId.HasValue || c.CatalogBrandId == brandId) &&
            (!typeId.HasValue || c.CatalogTypeId == typeId));
        }
    }
}
