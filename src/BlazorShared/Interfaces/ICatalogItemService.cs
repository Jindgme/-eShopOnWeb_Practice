using BlazorShared.Models;
using BlazorShared.Models.Requests;

namespace BlazorShared.Interfaces
{
    public interface ICatalogItemService
    {
        Task<CatalogItem> Create(CreateCatalogItemRequest request);
        Task<List<CatalogItem>> ListPaged(int pageSize);
        Task<List<CatalogItem>> List();
        Task<CatalogItem> Edit(CatalogItem catalogItem);
        Task<CatalogItem> GetById(int id);
        Task<string> Delete(int id);
    }
}
