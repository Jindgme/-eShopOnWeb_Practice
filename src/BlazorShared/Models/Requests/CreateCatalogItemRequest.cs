using System.ComponentModel.DataAnnotations;

namespace BlazorShared.Models.Requests
{
    public class CreateCatalogItemRequest
    {
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        [Required(ErrorMessage ="名称是必填项")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "描述是必填项")]
        public string Description { get; set; } = string.Empty;
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "价格必须是一个有效的货币值，最多两位小数")]
        [Range(typeof(decimal),"0.01","10000",ErrorMessage = "价格必须在0.01到10000之间")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }=0;

        public string PictureUri { get; set; }=string.Empty;
        public string PictureBase64 { get; set; } = string.Empty;
        public string PictureName { get; set; } = string.Empty;
    }
}
