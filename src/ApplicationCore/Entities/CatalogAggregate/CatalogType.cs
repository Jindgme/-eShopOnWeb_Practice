using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.CatalogAggregate
{
    // 商品类型实体
    public class CatalogType:BaseEntity,IAggregateRoot
    {
        public string Type { get; private set; }
        public CatalogType(string type)
        {
            Type=type;
        }
    }
}
