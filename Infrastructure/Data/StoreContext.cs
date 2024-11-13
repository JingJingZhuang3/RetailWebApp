using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// This class manages the database connection and is responsible for querying and saving data.
/// </summary>
/// <param name="options">SQL Server connection string</param>
public class StoreContext(DbContextOptions options) : DbContext(options)
{
    /* 
     * DbSet<Product> Products: This property represents a collection of Product entities. 
     * It maps to a table in the database and allows for CRUD operations on Product records.
     */
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //This approach allows for centralized management of all entity configuration classes, enhancing modularity and ease of maintenance.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
