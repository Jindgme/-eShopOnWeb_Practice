using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.CatalogAggregate
{
    //商品品牌实体
    public class CatalogBrand:BaseEntity,IAggregateRoot
    {
        public string Brand { get;private set; }
        public CatalogBrand(string brand)
        {
            Brand=brand;
        }
    }
}
