using Catalog.API.Extensions;
using Shared.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Add Database
services.AddCatalogMongoDatabaseContext(builder.Configuration);

// Add Mongo Repositories
services.AddCatalogRepositories();

// Add Services
services.AddServices();

// Add User Info
services.AddUserInfo();

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
