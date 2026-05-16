using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogItemEndpoints.DeleteCatalogItem
{
    public class DeleteCatalogItemEndpoint : IEndpoint<IResult, DeleteCatalogItemRequest, IRepository<CatalogItem>>
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<DeleteCatalogItemEndpoint> _logger;

        public DeleteCatalogItemEndpoint(IWebHostEnvironment env, ILogger<DeleteCatalogItemEndpoint> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/catalog-items/{id}", HandleAsync)
                .Produces<DeleteCatalogItemResponse>()
                .WithTags("CatalogItemEndpoints");
        }

        public async Task<IResult> HandleAsync([AsParameters] DeleteCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
        {
            var response = new DeleteCatalogItemResponse(request.CorrelationId());
            var item = await itemRepository.GetByIdAsync(request.Id);
            if (item == null)
            {
                return Results.NotFound();
            }
            // 删除本地图片
            if (!string.IsNullOrEmpty(item.PictureUri) && item.PictureUri != "eCatalog-item-default.png")
            {
                DeleteOldImage(item.PictureUri);
            }

            await itemRepository.DeleteAsync(item);
            return Results.Ok(response);
        }
        private void DeleteOldImage(string pictureUri)
        {
            if (string.IsNullOrEmpty(pictureUri)) return;
            var fileName = Path.GetFileName(pictureUri);

            try
            {
                var apiRoot = _env.ContentRootPath;
                var webRoot = Path.GetFullPath(Path.Combine(apiRoot, "..", "web", "wwwroot", "images", "test-products"));
                var fullFilePath = Path.Combine(webRoot, fileName);
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除商品项时删除图片失败：{ex.Message}");
            }
        }
    }
}
