using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogItemEndpoints.CreateCatalogItem
{
    public class CreateCatalogItemEndpoint : IEndpoint<IResult, CreateCatalogItemRequest, IRepository<CatalogItem>>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IWebHostEnvironment _env;

        public CreateCatalogItemEndpoint(IUriComposer uriComposer, IWebHostEnvironment env)
        {
            _uriComposer = uriComposer;
            _env = env;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapPost("api/catalog-items", HandleAsync)
                .Produces<CreateCatalogItemResponse>()
                .WithDescription("创建一个新的商品项。")
                .WithTags("CatalogItemEndpoints");
        }

        public async Task<IResult> HandleAsync([FromBody]CreateCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
        {
            var response = new CreateCatalogItemResponse(request.CorrelationId());

            var catalogItemNameSpec = new CatalogItemNameSpecification(request.Name);
            var existingCatalogItem = await itemRepository.CountAsync(catalogItemNameSpec);
            if (existingCatalogItem > 0)
            {
                throw new DuplicateException($"{request.Name}商品项已经存在。");
            }

            var newItem = new CatalogItem(request.CatalogTypeId, request.CatalogBrandId, request.Name, request.Description, request.Price, request.PictureUri);
            newItem = await itemRepository.AddAsync(newItem);

            if (newItem.Id != 0)
            {
                string imageName = $"{newItem.Id}.png";
                if (!string.IsNullOrEmpty(request.PictureUri))
                {
                    await SaveImageAsync(request.PictureUri, imageName);
                    newItem.UpdateTestPictureUri(imageName);
                }
                else
                {
                    newItem.UpdatePictureUri("eCatalog-item-default.png");
                }
                await itemRepository.UpdateAsync(newItem);
            }

            var dto = new CatalogItemDto
            {
                Id = newItem.Id,
                CatalogBrandId = newItem.CatalogBrandId,
                CatalogTypeId = newItem.CatalogTypeId,
                Name = newItem.Name,
                Description = newItem.Description,
                Price = newItem.Price,
                PictureUri = _uriComposer.ComposePictureUri(newItem.PictureUri)
            };
            response.CatalogItem = dto;
            return Results.Created($"api/catalog-items/{dto.Id}", response);
        }
        /// <summary>
        /// 将Base64编码的图片数据保存为文件。首先，检查Base64字符串是否包含逗号，如果包含，则分割字符串并获取逗号后的部分作为实际的Base64数据。
        /// 然后，将Base64字符串转换为字节数组，并将其写入指定路径的文件中。
        /// 文件路径由当前工作目录、"wwwroot/images/test-products"目录和提供的文件名组成。
        /// </summary>
        /// <param name="base64Data"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task SaveImageAsync(string base64Data,string fileName)
        {
            var base64DataParts = base64Data.Contains(',') ? base64Data.Split(",")[1]: base64Data;
            byte[] bytes=Convert.FromBase64String(base64DataParts);

            // 开发环境
            var apiRoot = _env.ContentRootPath;
            var webRoot = Path.GetFullPath(Path.Combine(apiRoot,"..","Web", "wwwroot", "images", "test-products"));
            if(!Directory.Exists(webRoot)) Directory.CreateDirectory(webRoot);
            var fullFilePath = Path.Combine(webRoot, fileName);

            await File.WriteAllBytesAsync(fullFilePath, bytes);
        }
        
    }
}
