using ApplicationCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Queries;
using Infrastructure.Logging;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

           
            services.Configure<CatalogSettings>(configuration.GetSection("CatalogSettings"));
            services.AddSingleton<IUriComposer, UriComposer>();
            //var catalogSettings= configuration.Get<CatalogSettings>() ?? new CatalogSettings();
            //services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IBasketQueryService, BasketQueryService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
