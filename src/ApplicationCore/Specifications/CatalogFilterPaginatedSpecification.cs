using ApplicationCore.Entities.CatalogAggregate;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    // 分页并过滤规约
    public class CatalogFilterPaginatedSpecification:Specification<CatalogItem>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip">跳过的记录数</param>
        /// <param name="take">获取的记录数</param>
        /// <param name="brandId"></param>
        /// <param name="typeId"></param>
        public CatalogFilterPaginatedSpecification(int skip,int take,int? brandId,int? typeId) 
        {
            if (take == 0)
            {
                take=int.MaxValue; // 不分页
            }
            Query
                .Where(c=>(!brandId.HasValue || c.CatalogBrandId==brandId)&&
                (!typeId.HasValue || c.CatalogTypeId==typeId))
                .Skip(skip).Take(take);
                
        }
    }
}
