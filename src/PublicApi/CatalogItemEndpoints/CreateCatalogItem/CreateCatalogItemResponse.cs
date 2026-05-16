using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.CreateCatalogItem
{
    public class CreateCatalogItemResponse: BaseResponse
    {
        public CreateCatalogItemResponse(Guid correlationId) : base(correlationId)
        {

        }
        public CreateCatalogItemResponse()
        {
            
        }
        public CatalogItemDto CatalogItem { get; set; }
    }
}
