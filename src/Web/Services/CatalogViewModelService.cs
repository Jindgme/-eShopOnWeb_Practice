using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly ILogger<CatalogViewModelService> _logger;
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IRepository<CatalogBrand> _brandRepository;
        private readonly IRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;

        public CatalogViewModelService(
            ILogger<CatalogViewModelService> logger,
            IRepository<CatalogItem> itemRepository,
            IRepository<CatalogBrand> brandRepository,
            IRepository<CatalogType> typeRepository,
            IUriComposer uriComposer)
        {
            _logger = logger;
            _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _uriComposer = uriComposer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">当前请求的页码</param>
        /// <param name="itemsPage">每页显示的数量</param>
        /// <param name="brandId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
        {
            _logger.LogInformation("调用了GetCatalogItems");
            var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
            var filterPaginatedSpecification =
                new CatalogFilterPaginatedSpecification(pageIndex * itemsPage, itemsPage, brandId, typeId);
            // 当前页的商品列表
            var itemsOnPage = await _itemRepository.ListAsync(filterPaginatedSpecification);
            // 不含分页的总记录数
            var totalItems=await _itemRepository.CountAsync(filterSpecification);

            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = _uriComposer.ComposePictureUri(i.PictureUri),
                    Price = i.Price,
                }).ToList(),
                Brands = (await GetBrands()).ToList(),
                Types = (await GetTypes()).ToList(),
                BrandFilterApplied = brandId,
                TypesFilterApplied = typeId,
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = totalItems,
                    ItemsPerPage = itemsOnPage.Count,
                    ActualPage = pageIndex,
                    TotalPages =(int)Math.Ceiling((double)totalItems / itemsPage)
                }
            };
            // Next 禁用条件：当前页是最后一页（ActualPage == TotalPages - 1）
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            // Previous 禁用条件：当前页是第一页（ActualPage == 0）
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            return vm;

        }
        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("调用了GetBrands");
            var brands = await _brandRepository.ListAsync();
            var items=brands
                        .Select(b=>new SelectListItem
                        {
                            Value=b.Id.ToString(),
                            Text=b.Brand
                        })
                        .OrderBy(b=>b.Text)
                        .ToList();
            // 在列表最前面插入一个所有项（空），表示不进行筛选
            var allItem = new SelectListItem
            {
                Value = null,
                Text = "All",
                Selected = true
            };
            items.Insert(0, allItem);
            return items;
        }
        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("调用了GetTypes");
            var types=await _typeRepository.ListAsync();
            var items=types.
                Select(t=>new SelectListItem
                {
                    Value=t.Id.ToString(),
                    Text=t.Type
                })
                .OrderBy(t=>t.Text)
                .ToList();
            var allItem = new SelectListItem
            {
                Value = null,
                Text = "All",
                Selected = true
            };
            items.Insert(0, allItem);
            return items;
        }
    }
}
