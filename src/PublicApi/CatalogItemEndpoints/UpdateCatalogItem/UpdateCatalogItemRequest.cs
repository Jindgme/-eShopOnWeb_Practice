using PublicApi.CorrelationMessage;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.CatalogItemEndpoints.UpdateCatalogItem
{
    public class UpdateCatalogItemRequest:BaseRequest
    {
        public int Id   { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(typeof(decimal),"0.01","10000")]
        public decimal Price { get; set; }

        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        public string PictureUri { get; set; }
        public string PictureBase64 { get; set; }
        public string PictureName { get; set; }
    }
}
