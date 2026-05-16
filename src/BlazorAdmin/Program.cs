using BlazorAdmin;
using BlazorAdmin.Services;
using BlazorShared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#admin");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region 配置BaseUrl，供HttpClient使用，并且在HttpService中注入IOptions<BaseUrlConfiguration>以获取BaseUrl
var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
builder.Services.Configure<BaseUrlConfiguration>(configSection);
#endregion

builder.Services.AddBlazorServices();
builder.Services.AddScoped<HttpService>();
builder.Services.AddScoped<ToastService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
