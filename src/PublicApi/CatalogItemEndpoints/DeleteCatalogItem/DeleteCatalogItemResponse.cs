using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.DeleteCatalogItem
{
    public class DeleteCatalogItemResponse:BaseResponse
    {
        public DeleteCatalogItemResponse(Guid correlationId):base(correlationId)
        {
            
        }
        public string Status { get; set; } = "已删除";
    }
}
