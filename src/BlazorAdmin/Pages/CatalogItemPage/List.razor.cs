using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAdmin.Pages.CatalogItemPage
{
    public partial class List:BlazorComponent
    {
        [Inject]
        public ICatalogItemService CatalogItemService { get; set; }
        [Inject]
        public ICatalogLookupDataService<CatalogBrand> CatalogBrandService { get; set; }
        [Inject]
        public ICatalogLookupDataService<CatalogType> CatalogTypeService { get; set; }

        private List<CatalogItem> catalogItems = new List<CatalogItem>();
        private List<CatalogBrand> catalogBrands = new List<CatalogBrand>();
        private List<CatalogType> catalogTypes= new List<CatalogType>();
        private Create CreateComponent { get; set; }
        private Edit EditComponent { get; set; }
        private Delete DeleteComponent { get; set; }

        // 在组件首次渲染后，异步加载目录项数据。
        // 通过调用 CatalogItemService 的 List 方法获取目录项列表，并将结果存储在 catalogItems 变量中。
        // 调用 StateHasChanged 方法通知 Blazor 组件状态已更改，需要重新渲染 UI。
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                catalogItems=await CatalogItemService.List();
                catalogBrands=await CatalogBrandService.List();
                catalogTypes=await CatalogTypeService.List();

                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task CreateClick()
        {
            await CreateComponent.Open();
        }
        private async Task UpdateClick(int id)
        {
            await EditComponent.Open(id);
        }
        private async Task DeleteClick(int id)
        {
            await DeleteComponent.Open(id);
        }
        // 当目录项被删除时，调用 ReloadCatalogItems 方法重新加载目录项数据。
        public async Task ReloadCatalogItems()
        {
            catalogItems = await CatalogItemService.List();
            StateHasChanged();
        }
    }
}
