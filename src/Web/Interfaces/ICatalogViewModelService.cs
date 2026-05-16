using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ICatalogViewModelService
    {
        /// <summary>
        /// 获取分页和筛选后的商品列表
        /// </summary>
        /// <param name="pageIndex">当前请求的页码</param>
        /// <param name="itemsPage">每页显示的商品数量</param>
        /// <param name="brandId">按品牌筛选（可选）</param>
        /// <param name="typeId">按类型筛选（可选）</param>
        /// <returns></returns>
        Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
