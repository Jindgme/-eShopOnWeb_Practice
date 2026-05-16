using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogItemEndpoints.UpdateCatalogItem
{
    public class UpdateCatalogItemEndpoint : IEndpoint<IResult, UpdateCatalogItemRequest, IRepository<CatalogItem>>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UpdateCatalogItemEndpoint> _logger;

        public UpdateCatalogItemEndpoint(IUriComposer uriComposer, IWebHostEnvironment env, ILogger<UpdateCatalogItemEndpoint> logger)
        {
            _uriComposer = uriComposer;
            _env = env;
            _logger = logger;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapPut("api/catalog-items", HandleAsync)
                .Produces<UpdateCatalogItemResponse>()
                .WithTags("CatalogItemEndpoints");
        }

        public async Task<IResult> HandleAsync([FromBody] UpdateCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
        {
            var response = new UpdateCatalogItemResponse(request.CorrelationId());
            var existingItem = await itemRepository.GetByIdAsync(request.Id);
            if (existingItem == null)
            {
                return Results.NotFound();
            }
            CatalogItem.CatalogItemDetails details = new(request.Name, request.Description, request.Price);
            existingItem.UpdateDetails(details);
            existingItem.UpdateType(request.CatalogTypeId);
            existingItem.UpdateBrand(request.CatalogBrandId);

            #region 更改图片
            if (!string.IsNullOrEmpty(request.PictureUri))
            {
                // 删除图片，保留默认图。其实也可以不用，因为该路径没有默认图，嘻嘻，我换路径了
                if (!string.IsNullOrEmpty(existingItem.PictureUri) && existingItem.PictureUri != "eCatalog-item-default.png")
                {
                    DeleteOldImage(existingItem.PictureUri);
                }
                // 用新图片的名称
                string imageName = string.IsNullOrEmpty(request.PictureName) ? $"{existingItem.Id}.png" : request.PictureName;
                await SaveImageAsync(request.PictureUri, imageName);
                existingItem.UpdateTestPictureUri(imageName);
            }
            #endregion
            await itemRepository.UpdateAsync(existingItem);
            var dto = new CatalogItemDto
            {
                Id = existingItem.Id,
                Name = existingItem.Name,
                Description = existingItem.Description,
                Price = existingItem.Price,
                CatalogBrandId = existingItem.CatalogBrandId,
                CatalogTypeId = existingItem.CatalogTypeId,
                PictureUri = _uriComposer.ComposePictureUri(existingItem.PictureUri)
            };
            response.CatalogItem = dto;
            return Results.Ok(response);
        }
        // 保存图片到Web\wwwroot\images\test-products下
        private async Task SaveImageAsync(string base64Data, string fileName)
        {
            var base64DataParts = base64Data.Contains(',') ? base64Data.Split(separator: ",")[1] : base64Data;
            byte[] bytes = Convert.FromBase64String(base64DataParts);

            var fullFilePath = GetFullFileName(fileName);

            await File.WriteAllBytesAsync(fullFilePath, bytes);
        }
        // 删除test-products路径下的图片
        private void DeleteOldImage(string pictureUri)
        {
            if(string.IsNullOrEmpty(pictureUri)) return;

            string fileName = Path.GetFileName(pictureUri);
            try
            {
                var fullFilePath = GetFullFileName(fileName);
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"更改图片时删除旧图片失败：{ex.Message}");
            }
            
        }
        // **************开发环境获取本地路径***************
        private string GetFullFileName(string fileName)
        {
            var apiRoot = _env.ContentRootPath;
            var webRoot = Path.GetFullPath(Path.Combine(apiRoot, "..", "Web", "wwwroot", "images", "test-products"));
            if (!Directory.Exists(webRoot)) Directory.CreateDirectory(webRoot);
            return Path.Combine(webRoot, fileName);
        }
    }
}
