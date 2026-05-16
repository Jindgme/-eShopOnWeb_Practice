using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogDbContext db,
            ILogger logger,
            int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                // 如果是sqlserver，则执行数据库迁移（包括创建和更新数据库）
                if (db.Database.IsSqlServer())
                {
                    db.Database.Migrate();
                }
                // AnyAsync判断是否存在任何记录，如果为空，则为false。
                // 如果改表为空，则添加数据
                if (!await db.CatalogBrands.AnyAsync())
                {
                    await db.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());
                    await db.SaveChangesAsync();
                }
                if (!await db.CatalogTypes.AnyAsync())
                {
                    await db.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());
                    await db.SaveChangesAsync();
                }
                if (!await db.CatalogItems.AnyAsync())
                {
                    await db.CatalogItems.AddRangeAsync(GetPreconfiguredItems()); ;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // 利用递归添加重试机制，应对部署服务器启动的时候产生的数据库延迟，断网等一系列问题。
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;
                logger.LogError(ex.Message);
                await SeedAsync(db, logger, retryForAvailability);
                
                throw;
            }

        }
        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand("Azure"),
                new CatalogBrand(".NET"),
                new CatalogBrand("Visual Studio"),
                new CatalogBrand("SQL Server"),
                new CatalogBrand("Other")
            };
        }
        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new("Mug"),
                new("T-Shirt"),
                new("Sheet"),
                new("USB Memory Stick")
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>
            {
                new(2,2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new(1,2, ".NET Black & White Mug", ".NET Black & White Mug", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new(2,5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "http://catalogbaseurltobereplaced/images/products/3.png"),
                new(2,2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(3,5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new(2,2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new(2,5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new(2,5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new(1,5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new(3,2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new(2,5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png")
            };
        }
    }
}
