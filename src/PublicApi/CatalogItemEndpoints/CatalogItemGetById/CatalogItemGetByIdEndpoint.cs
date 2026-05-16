using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogItemEndpoints.CatalogItemGetById
{
    public class CatalogItemGetByIdEndpoint : IEndpoint<IResult, GetByIdCatalogItemRequest, IRepository<CatalogItem>>
    {
        private readonly IUriComposer _uriComposer;

        public CatalogItemGetByIdEndpoint(IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/catalog-items/{catalogItemId}", HandleAsync)
                .Produces<GetByIdCatalogItemResponse>()
                .WithTags("CatalogItemEndpoints");
        }

        public async Task<IResult> HandleAsync([AsParameters]GetByIdCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
        {
            var response = new GetByIdCatalogItemResponse(request.CorrelationId());

            var item = await itemRepository.GetByIdAsync(request.CatalogItemId);
            if (item == null)
            {
                return Results.NotFound();
            }
            response.CatalogItem = new CatalogItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CatalogBrandId = item.CatalogBrandId,
                CatalogTypeId = item.CatalogTypeId,
                PictureUri = _uriComposer.ComposePictureUri(item.PictureUri)
            };
            return Results.Ok(response);
        }
    }
}
