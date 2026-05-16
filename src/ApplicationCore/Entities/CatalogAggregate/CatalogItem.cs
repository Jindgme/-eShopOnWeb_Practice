using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.CatalogAggregate
{
    /// <summary>
    /// 商品项实体
    /// </summary>
    public class CatalogItem:BaseEntity,IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get;private set; }
        public decimal Price { get; private set; }
        public string PictureUri { get; private set; } // 商品图片URI

        public int CatalogTypeId { get; private set; }
        public CatalogType? CatalogType { get; private set; }
        public int CatalogBrandId { get; private set; }
        public CatalogBrand? CatalogBrand { get; private set; }


        public CatalogItem(int catalogTypeId,
            int catalogBrandId,
            string name,
            string description,
            decimal price,
            string pictureUri)
        {
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            Name = name;
            Description = description;
            Price = price;
            PictureUri = pictureUri;
        }
        public void UpdateDetails(CatalogItemDetails details)
        {
            Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
            Guard.Against.NullOrEmpty(details.Description,nameof(details.Description));
            //验证Price>0,不能为负数或0
            Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));
            Name= details.Name;
            Description= details.Description;
            Price = details.Price;
        }
        public void UpdateType(int catalogTypeId)
        {
            // 验证参数catalogTypeId不能为0
            Guard.Against.Zero(catalogTypeId,nameof(catalogTypeId));
            CatalogTypeId = catalogTypeId;
        }
        public void UpdateBrand(int catalogBrandId)
        {
            Guard.Against.Zero(catalogBrandId, nameof(catalogBrandId));
            CatalogBrandId= catalogBrandId;
        }
        public void UpdatePictureUri(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                PictureUri=string.Empty;
                return;
            }
            PictureUri = $"images\\products\\{pictureName}";
        }
        public void UpdateTestPictureUri(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = $"images\\test-products\\{pictureName}";  // 官方后面会带 ?{new DateTime().Ticks} 。个人感觉没啥用，还会影响后面删除图片，所以删了
        }
        /// <summary>
        /// 商品目录实体DTO
        /// </summary>
        public readonly record struct CatalogItemDetails
        {
            public string? Name { get; }
            public string? Description { get; }
            public decimal Price { get; }
            public CatalogItemDetails(string? name, string? description, decimal price)
            {
                Name = name;
                Description = description;
                Price = price;
            }
        }
    }
}
