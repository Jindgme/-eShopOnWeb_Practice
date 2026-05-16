using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Interfaces;
using AutoMapper;
using PublicApi.EndpointExtensions;

namespace PublicApi.CatalogBrandEndpoints
{
    public class CatalogBrandListEndpoint : IEndpoint<IResult, IRepository<CatalogBrand>>
    {
        private readonly IMapper _mapper;

        public CatalogBrandListEndpoint(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/catalog-brands", HandleAsync)
                .Produces<ListCatalogBrandsResponse>()
                .WithTags("CatalogBrandEndpoints");
        }

        public async Task<IResult> HandleAsync(IRepository<CatalogBrand> request)
        {
            var items= await request.ListAsync();

            //var itemsDto=items.Select(x => new CatalogBrandDto
            //{
            //    Id = x.Id,
            //    Name = x.Brand
            //}).ToList();
            var itemsDto = items.Select(item=>_mapper.Map<CatalogBrandDto>(item));
            var result = new ListCatalogBrandsResponse
            {
                CatalogBrands = itemsDto.ToList()
            };
            return Results.Ok(result);
        }
    }
}
