using BlazorShared.Interfaces;
using System.Text.Json.Serialization;

namespace BlazorShared.Models.Response
{
    public class CatalogTypeResponse : ILookupDataResponse<CatalogType>
    {
        [JsonPropertyName("CatalogTypes")]
        public List<CatalogType> List { get;set; } = new List<CatalogType>();
    }
}
