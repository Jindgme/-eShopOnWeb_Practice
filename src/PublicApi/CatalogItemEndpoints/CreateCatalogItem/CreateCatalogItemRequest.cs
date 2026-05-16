using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.CreateCatalogItem
{
    public class CreateCatalogItemRequest:BaseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        public string PictureUri { get; set; }
        public string PictureBase64 { get; set; }
        public string PictureName { get; set; }

    }
}
