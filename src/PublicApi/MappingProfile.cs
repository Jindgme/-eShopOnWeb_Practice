using ApplicationCore.Entities.CatalogAggregate;
using AutoMapper;
using PublicApi.CatalogBrandEndpoints;
using PublicApi.CatalogItemEndpoints;
using PublicApi.CatalogTypeEndpoints;

namespace PublicApi
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogItem, CatalogItemDto>();
            CreateMap<CatalogBrand, CatalogBrandDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Brand));
            CreateMap<CatalogType, CatalogTypeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Type));
        }
    }
}
