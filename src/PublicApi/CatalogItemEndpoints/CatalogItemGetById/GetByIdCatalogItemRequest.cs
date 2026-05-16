using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.CatalogItemGetById
{
    public class GetByIdCatalogItemRequest:BaseRequest
    {
        public int CatalogItemId { get; init; }
    }
}
