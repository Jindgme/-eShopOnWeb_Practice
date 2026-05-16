using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// 官方还写了IReadRepository接口。但是在Ardalis库中，IRepositoryBase继承了IReadRepositoryBase接口（查询），所以这里就不写了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public Repository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
