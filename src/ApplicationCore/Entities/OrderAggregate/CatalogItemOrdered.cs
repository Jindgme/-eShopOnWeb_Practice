using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.OrderAggregate
{
    // 已订购商品实体
    public class CatalogItemOrdered
    {
        public CatalogItemOrdered(int catalogItemId, string productName, string pictureUri)
        {
            Guard.Against.OutOfRange(catalogItemId,nameof(catalogItemId),1,int.MaxValue);
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));

            CatalogItemId = catalogItemId;
            ProductName = productName;
            PictureUri = pictureUri;
        }
        private CatalogItemOrdered()
        {
        }
        public int CatalogItemId { get;private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
    }
}
