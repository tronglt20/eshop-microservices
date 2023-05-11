using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;

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

services.AddMassTransit(_ =>
{
    _.AddConsumer<BasketCheckoutConsumer>();
    _.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(config["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint("basketcheckout-queue", c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});
services.AddScoped<BasketCheckoutConsumer>();

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
