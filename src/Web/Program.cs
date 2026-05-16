using Infrastructure;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


var config = builder.Configuration;
builder.Services.ConfigureServices(config);
builder.Services.AddCoreServices(config);
builder.Services.AddWebServices();

var app = builder.Build();

await app.AddSeedData();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute("default",
    "{controller=Home}/{action=Index}/{id?}");

//app.MapFallbackToFile("index.html");

app.Run();
