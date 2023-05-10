using eshop.Client.Dtos;
using eshop.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .GetSection("APIEndpoints")
    .Get<APIEndpoints>(options => options.BindNonPublicProperties = true);

builder.Services.AddRazorPages();

builder.Services.AddServices();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.Run();
