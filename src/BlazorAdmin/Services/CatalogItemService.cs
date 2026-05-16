using BlazorShared.Interfaces;
using BlazorShared.Models;
using BlazorShared.Models.Requests;
using BlazorShared.Models.Response;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services
{
    public class CatalogItemService : ICatalogItemService
    {
        private readonly ICatalogLookupDataService<CatalogBrand> _brandService;
        private readonly ICatalogLookupDataService<CatalogType> _typeService;
        private readonly ILogger<CatalogItemService> _logger;
        private readonly HttpService _httpService;
        public CatalogItemService(ICatalogLookupDataService<CatalogBrand> brandService,
            ICatalogLookupDataService<CatalogType> typeService,
            ILogger<CatalogItemService> logger,
            HttpService httpService)
        {
            _brandService = brandService;
            _typeService = typeService;
            _logger = logger;
            _httpService = httpService;
        }

        public async Task<CatalogItem> Create(CreateCatalogItemRequest catalogItem)
        {
            var result= await _httpService.HttpPost<CreateCatalogItemResponse>("catalog-items", catalogItem);
            return result?.CatalogItem;
        }

        public async Task<string> Delete(int id)
        {
            var result = await _httpService.HttpDelete<DeleteCatalogItemResonse>("catalog-items",id);
            return result.Status;
        }

        public async Task<CatalogItem> Edit(CatalogItem catalogItem)
        {
            var result = await _httpService.HttpPut<EditCatalogItemResponse>("catalog-items", catalogItem);
            return result?.CatalogItem;
        }

        public async Task<CatalogItem> GetById(int id)
        {
            var brandListTask = _brandService.List();
            var typeListTask= _typeService.List();
            var itemGetTask = _httpService.HttpGet<EditCatalogItemResponse>($"catalog-items/{id}");
            await Task.WhenAll(brandListTask, typeListTask, itemGetTask);
            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var catalogItem = itemGetTask.Result.CatalogItem;
            catalogItem.CatalogBrand = brands.First(b => b.Id == catalogItem.CatalogBrandId).Name;
            catalogItem.CatalogType = types.First(t => t.Id == catalogItem.CatalogTypeId).Name;
            return catalogItem;
        }

        public async Task<List<CatalogItem>> List()
        {
            _logger.LogInformation("正在从API中获取catalogItems");

            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            var itemListTask =_httpService.HttpGet<PagedCatalogItemResponse>("catalog-items");

            await Task.WhenAll(brandListTask, typeListTask, itemListTask);

            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var items = itemListTask.Result.CatalogItems;
            foreach (var item in items)
            {
                item.CatalogBrand = brands.FirstOrDefault(b => b.Id == item.CatalogBrandId)?.Name;
                item.CatalogType = types.FirstOrDefault(t => t.Id == item.CatalogTypeId)?.Name;
            }
            return items;
        }

        public async Task<List<CatalogItem>> ListPaged(int pageSize)
        {
            _logger.LogInformation("正在从API中分页获取catalogItems");

            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            var itemListTask = _httpService.HttpGet<PagedCatalogItemResponse>("catalog-items?PageSize=5");

            await Task.WhenAll(brandListTask, typeListTask, itemListTask);

            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var items = itemListTask.Result.CatalogItems;
            foreach (var item in items)
            {
                item.CatalogBrand = brands.FirstOrDefault(b => b.Id == item.CatalogBrandId)?.Name;
                item.CatalogType = types.FirstOrDefault(t => t.Id == item.CatalogTypeId)?.Name;
            }
            return items;
        }
    }
}
