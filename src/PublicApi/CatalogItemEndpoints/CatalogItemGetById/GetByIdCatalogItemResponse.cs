using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.CatalogItemGetById
{
    public class GetByIdCatalogItemResponse:BaseResponse
    {
        public GetByIdCatalogItemResponse(Guid correlationId):base(correlationId)
        {
            
        }
        public CatalogItemDto CatalogItem { get; set; } = new CatalogItemDto();
    }
}
