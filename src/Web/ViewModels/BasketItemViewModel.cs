using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="数量必须大于等于0")]
        public int Quantity { get; set; }  
        public int CatalogItemId { get; set; }
        public string? ProductName { get; set; }

        public string? PictureUrl { get; set; }
    }
}
