using Basket.API.Extensions;
using MassTransit;
using Shared.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var config = builder.Configuration;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddConfigSettings(config);

// Add UserInfo
services.AddUserInfo();

// Add Distributed cache
services.AddRedisCache();

// Add Repositories
services.AddBasketRepositories();

// Add Services
services.AddGrpcClients(config)
        .AddServices();

services.AddMassTransit(_ =>
{
    _.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(config["EventBusSettings:HostAddress"]);
    });
});

var app = builder.Build();

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
