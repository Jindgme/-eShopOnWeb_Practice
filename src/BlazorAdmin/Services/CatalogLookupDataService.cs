using BlazorShared;
using BlazorShared.Attributes;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorAdmin.Services
{
    public class CatalogLookupDataService<TLookupData, TReponse> : ICatalogLookupDataService<TLookupData>
        where TLookupData : LookupData
        where TReponse : ILookupDataResponse<TLookupData>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogLookupDataService<TLookupData, TReponse>> _logger;
        private readonly string _apiUrl;

        public CatalogLookupDataService(HttpClient httpClient, 
            ILogger<CatalogLookupDataService<TLookupData, TReponse>> logger,
            IOptions<BaseUrlConfiguration> baseUrl)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiUrl = baseUrl.Value.ApiBase;
        }
        /// <summary>
        ///  抓取目录查找数据列表。这个方法会根据 TLookupData 的类型获取相应的 API 端点，并从 API 获取数据。
        ///  日志记录了正在获取数据的类型和端点信息，以便于调试和监控。
        /// </summary>
        /// <returns></returns>
        public async Task<List<TLookupData>> List()
        {
            var endpoint = typeof(TLookupData).GetCustomAttribute<EndpointAttribute>()!.Name;
            _logger.LogInformation($"从API获取{typeof(TLookupData).Name}数据。端点：{endpoint}");

            // 从API获取数据，获取json并转换成c#对象，并返回列表。如果API返回null，则返回一个空列表。
            var response = await _httpClient.GetFromJsonAsync<TReponse>($"{_apiUrl}{endpoint}");
            return response?.List ?? new List<TLookupData>();
        }
    }
}
