using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogItemEndpoints.CatalogItemList
{
    public class ListPagedCatalogItemRequest:BaseRequest
    {
        //public ListPagedCatalogItemRequest(int? pageSize, int? pageIndex, int? catalogBrandId, int? catalogTypeId)
        //{
        //    PageSize = pageSize ?? 0;
        //    PageIndex = pageIndex ?? 0;
        //    CatalogBrandId = catalogBrandId;
        //    CatalogTypeId = catalogTypeId;
        //}

        public int? PageSize { get; set; } // 每页显示的商品数量
        public int? PageIndex { get; set; }  // 当前页码，从0开始
        public int? CatalogBrandId { get; set; }
        public int? CatalogTypeId { get; set; }
        
    }
}
