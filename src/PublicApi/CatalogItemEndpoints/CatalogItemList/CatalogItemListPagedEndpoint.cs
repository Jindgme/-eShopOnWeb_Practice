using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogItemEndpoints.CatalogItemList
{
    public class CatalogItemListPagedEndpoint : IEndpoint<IResult, ListPagedCatalogItemRequest, IRepository<CatalogItem>>
    {
        private readonly IMapper _mapper;
        private readonly IUriComposer _uriComposer;

        public CatalogItemListPagedEndpoint(IMapper mapper, IUriComposer uriComposer)
        {
            _mapper = mapper;
            _uriComposer = uriComposer;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/catalog-items",
            //    async (int? pageSize,int? pageIndex,int? catalogBrandId,int? catalogTypeId, IRepository<CatalogItem> itemRepository) =>
            //{
            //    return await HandleAsync(new ListPagedCatalogItemRequest(pageSize, pageIndex,catalogBrandId,catalogTypeId), itemRepository);
            //}
            HandleAsync
            )
                .Produces<ListPagedCatalogItemResponse>()
                .WithTags("CatalogItemEndpoints");
        }

        public async Task<IResult> HandleAsync([AsParameters] ListPagedCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
        {
            var response = new ListPagedCatalogItemResponse(request.CorrelationId());

            // 根据品牌和类型筛选出商品总数
            var filterSpec = new CatalogFilterSpecification(request.CatalogBrandId, request.CatalogTypeId);
            var totalItems = await itemRepository.CountAsync(filterSpec);

            var pageIndex = request.PageIndex ?? 0;
            var pageSize = request.PageSize ?? 0;

            var pageSpec = new CatalogFilterPaginatedSpecification(
                skip: pageIndex * pageSize,
                take: pageSize,
                brandId: request.CatalogBrandId,
                typeId: request.CatalogTypeId);
            // 根据品牌和类型筛选出当前页的商品列表
            var items = await itemRepository.ListAsync(pageSpec);
            // 将商品列表映射为 DTO 并添加到响应中  
            response.CatalogItems.AddRange(items.Select(_mapper.Map<CatalogItemDto>));

            foreach (CatalogItemDto item in response.CatalogItems)
            {
                item.PictureUri = _uriComposer.ComposePictureUri(item.PictureUri);
            }
            if (request.PageSize > 0)
            {
                response.PageCount = (int)Math.Ceiling((double)totalItems / pageSize);
            }
            else
            {
                response.PageCount = totalItems > 0 ? 1 : 0;
            }
            return Results.Ok(response);
        }
    }
}
