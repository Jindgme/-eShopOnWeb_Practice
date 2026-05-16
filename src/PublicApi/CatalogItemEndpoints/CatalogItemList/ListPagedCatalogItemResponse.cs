using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.CatalogItemList
{
    public class ListPagedCatalogItemResponse:BaseResponse
    {
        public ListPagedCatalogItemResponse(Guid correlationId):base(correlationId)
        {
            
        }
        public ListPagedCatalogItemResponse()
        {
            
        }
        public List<CatalogItemDto> CatalogItems { get; set; } = new List<CatalogItemDto>();
        public int PageCount { get; set; }  // 总页数
    }
}
