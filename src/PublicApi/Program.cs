using BlazorShared;
using Infrastructure;
using PublicApi;
using PublicApi.Configuration;
using PublicApi.EndpointExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpoint();
builder.Services.AddCoreServices();

builder.Services.AddAutoMapper(_ => { },typeof(MappingProfile));

#region ≈‰÷√CORS
var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
builder.Services.Configure<BaseUrlConfiguration>(configSection);
var baseUrlConfig=configSection.Get<BaseUrlConfiguration>();
const string CORS_POLICY="CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS_POLICY, policy =>
    {
        policy.WithOrigins(baseUrlConfig!.WebBase!.TrimEnd('/'));
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(CORS_POLICY);

app.MapEndpoints();

app.Run();
