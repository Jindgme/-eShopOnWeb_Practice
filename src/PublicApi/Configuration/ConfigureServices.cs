using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;

namespace PublicApi.Configuration
{
    public static class ConfigureServices
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IUriComposer,UriComposer>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
