using Microsoft.EntityFrameworkCore;
using Ordering.API.Extensions;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddOrderDatabaseContext(config);

services.AddRepositories()
        .AddServices();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var servicesMigration = scope.ServiceProvider;
    var context = servicesMigration.GetRequiredService<OrderContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
