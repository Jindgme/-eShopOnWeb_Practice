using PublicApi.CorrelationMessage;

namespace PublicApi.CatalogBrandEndpoints
{
    public class ListCatalogBrandsResponse:BaseResponse
    {
        public ListCatalogBrandsResponse() { }
        public ListCatalogBrandsResponse(Guid correlationId) : base(correlationId)
        {
        }
        public List<CatalogBrandDto> CatalogBrands { get; set; } = new List<CatalogBrandDto>();
    }
}
