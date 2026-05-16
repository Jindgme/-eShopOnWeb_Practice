using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogTypeEndpoints
{
    public class ListCatalogTypesResponse:BaseResponse
    {
        public ListCatalogTypesResponse()
        {
            
        }
        public ListCatalogTypesResponse(Guid correlationId) : base(correlationId)
        {
        }
        public List<CatalogTypeDto> CatalogTypes { get; set; } = new List<CatalogTypeDto>();
    }
}
