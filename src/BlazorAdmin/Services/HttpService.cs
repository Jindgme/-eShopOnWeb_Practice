using BlazorShared;
using BlazorShared.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly ToastService _toastService;

        public HttpService(HttpClient httpClient,
            IOptions<BaseUrlConfiguration> baseUrl,
            ToastService toastService)
        {
            _httpClient = httpClient;
            _apiUrl = baseUrl.Value.ApiBase;
            _toastService = toastService;
        }
        // 这里主要是获取catalogItems的请求基础路径， "ApiBase": https://localhost:7033/api/catalogItems
        // 并反序列化成c#对象返回。供blazor前端调用，获取数据。
        public async Task<T> HttpGet<T>(string uri)
            where T : class
        {
            var result = await _httpClient.GetAsync($"{_apiUrl}{uri}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await FormHttpResponseMessage<T>(result);
        }
        public async Task<T> HttpPost<T>(string uri,object dataToSend)
            where T : class
        {
            var content=ToJson(dataToSend);
            var result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
                var exception = JsonSerializer.Deserialize<ErrorDetails>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                _toastService.ShowToast($"错误：{exception.Message}", ToastLevel.Error);
                return null;
            }
            return await FormHttpResponseMessage<T>(result);
        }
        public async Task<T> HttpPut<T>(string uri,object dataToSend)
            where T :class
        {
            var content=ToJson(dataToSend);
            var result = await _httpClient.PutAsync($"{_apiUrl}{uri}", content);
            if(!result.IsSuccessStatusCode)
            {
                _toastService.ShowToast("错误", ToastLevel.Error);
                return null;
            }
            return await FormHttpResponseMessage<T>(result);
        }
        public async Task<T> HttpDelete<T>(string uri,int id)
            where T :class
        {
            var result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await FormHttpResponseMessage<T>(result);
        }
        private async Task<T> FormHttpResponseMessage<T>(HttpResponseMessage result)
        {
            // 将HTTP响应消息的内容作为字符串读取，并使用JsonSerializer将其反序列化为指定类型T的对象。
            // JsonSerializerOptions中的PropertyNameCaseInsensitive设置为true，表示在反序列化时忽略JSON属性名称的大小写。这使得API返回的JSON属性名称与C#类属性名称大小写不一致时仍然能够正确反序列化。
            return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        private StringContent ToJson(object obj)
        {
            var json=JsonSerializer.Serialize(obj);
            Console.WriteLine($"json:{json}");
            // 严格按照JSON格式将对象序列化为字符串，并使用UTF-8编码和"application/json"媒体类型创建一个StringContent对象。
            return new StringContent(json,Encoding.UTF8,"application/json");
        }
    }
}
