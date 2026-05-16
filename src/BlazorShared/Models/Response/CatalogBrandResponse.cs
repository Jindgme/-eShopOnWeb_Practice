using BlazorShared.Interfaces;
using System.Text.Json.Serialization;

namespace BlazorShared.Models.Response
{
    // 定义一个响应类，用于返回 CatalogBrand 数据列表。
    // 它实现了 ILookupDataResponse 接口，确保它具有一个 List 属性来存储 CatalogBrand 对象的列表。
    public class CatalogBrandResponse : ILookupDataResponse<CatalogBrand>
    {
        [JsonPropertyName("CatalogBrands")]
        public List<CatalogBrand> List { get; set; } = new List<CatalogBrand>();
    }
}
