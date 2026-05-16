using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogViewModelService, CatalogViewModelService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            return services;
        }
    }
}
