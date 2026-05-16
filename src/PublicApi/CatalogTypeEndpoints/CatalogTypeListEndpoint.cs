using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using AutoMapper;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogTypeEndpoints
{
    public class CatalogTypeListEndpoint : IEndpoint<IResult, IRepository<CatalogType>>
    {
        private readonly IMapper _mapper;

        public CatalogTypeListEndpoint(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/catalog-types", HandleAsync)
                .Produces<ListCatalogTypesResponse>()
                .WithTags("CatalogTypeEndpoints");
        }

        public async Task<IResult> HandleAsync(IRepository<CatalogType> typesRepository)
        {
            var items =await typesRepository.ListAsync();
            var itemsDto = items.Select(item => _mapper.Map<CatalogTypeDto>(item));
            var result = new ListCatalogTypesResponse
            {
                CatalogTypes = itemsDto.ToList()
            };
            return Results.Ok(result);
        }
    }
}
