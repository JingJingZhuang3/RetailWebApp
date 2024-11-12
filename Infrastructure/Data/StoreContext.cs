using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Database connection
/// </summary>
/// <param name="options">SQL Server connection string</param>
public class StoreContext(DbContextOptions options) : DbContext(options)
{
    //Define the Product Entity
    public DbSet<Product> Products {get; set;}
}
