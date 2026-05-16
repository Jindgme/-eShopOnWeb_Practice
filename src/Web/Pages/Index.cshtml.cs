using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        public IndexModel(ICatalogViewModelService catalogViewModelService)
        {
            _catalogViewModelService = catalogViewModelService;
        }
        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        // pageId 表示当前请求的页码
        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            CatalogModel = await _catalogViewModelService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);
        }
    }
}
