using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            // Turns Json to list of Product Objects
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products == null) return;
            // Adds the list of products to the Products table in the database.
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
