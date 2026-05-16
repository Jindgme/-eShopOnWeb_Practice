using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("CatalogConnection"));
            });
        }
        public static async Task AddSeedData(this WebApplication app)
        {
            app.Logger.LogInformation("开始迁移种子数据......");
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var scopedProvider = scope.ServiceProvider;
                    var catalogContext = scopedProvider.GetRequiredService<CatalogDbContext>();
                    await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "在迁移种子数据的时候发生错误");
                    throw;
                }
            }
        }
    }
}
