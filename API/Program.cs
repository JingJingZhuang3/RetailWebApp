using API.Middleware;
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
//IGenericRepository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//CORs
builder.Services.AddCors();

var app = builder.Build();

// configure the HTTP request pipeline
app.UseMiddleware<ExceptionMiddleware>();

// allow the list orgins to send requests to API server
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();   // disposed once it's outside of scope
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    // apply pending migration to database, will create the database if it does not already exist.
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
