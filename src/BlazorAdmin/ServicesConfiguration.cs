using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using BlazorShared.Models.Response;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogItemService, CatalogItemService>();

            //services.AddScoped(typeof(ICatalogLookupDataService<>), typeof(CatalogLookupDataService<,>));
            // 当接口与实现类的泛型参数不完全相同时，无法直接使用上面这种方式注册，所以需要分别注册每个接口与实现类的组合
            services.AddScoped<ICatalogLookupDataService<CatalogBrand>, CatalogLookupDataService<CatalogBrand, CatalogBrandResponse>>();
            services.AddScoped<ICatalogLookupDataService<CatalogType>, CatalogLookupDataService<CatalogType, CatalogTypeResponse>>();


            return services;
        }
    }
}
