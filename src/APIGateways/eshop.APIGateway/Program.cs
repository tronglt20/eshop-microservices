using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
        .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseRouting();

await app.UseOcelot();

app.Run();


