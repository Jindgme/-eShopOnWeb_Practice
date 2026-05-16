
using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.DeleteCatalogItem
{
    public class DeleteCatalogItemRequest:BaseRequest
    {
        public int Id { get; set; }
    }
}
