using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.UpdateCatalogItem
{
    public class UpdateCatalogItemResponse:BaseResponse
    {
        public UpdateCatalogItemResponse(Guid correlationId):base(correlationId)
        {
            
        }
        public UpdateCatalogItemResponse()
        {
            
        }
        public CatalogItemDto CatalogItem { get; set; }
    }
}
