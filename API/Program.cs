using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// configures the StoreContext to use SQL Server as its database provider in an ASP.NET Core application. 
// It registers the StoreContext with the dependency injection container and retrieves the connection string named "DefaultConnection" from the application's configuration settings.
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Register IRepository: Scoped at the level of HTTP request lifetime
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
