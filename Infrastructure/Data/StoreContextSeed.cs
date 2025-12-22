using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
        {
                // if (products == null || productTypes == null || productBrands == null || productMakes == null
                // || productModels == null) return;
                if(!context.ProductTypes.Any())
                {
                    var productTypesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/producttypes.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    if (productTypes == null) return;
                    context.ProductTypes.AddRange(productTypes);
                    await context.SaveChangesAsync(); // Save to get generated Ids
                }
                if (!context.ProductBrands.Any())
                {
                    var productBrandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/productbrands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (productBrands == null) return;
                    context.ProductBrands.AddRange(productBrands);
                    await context.SaveChangesAsync();
                }
                if (!context.ProductMakes.Any())
                {
                    var productMakesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/productmakes.json");
                    var productMakes = JsonSerializer.Deserialize<List<ProductMake>>(productMakesData);
                    if (productMakes == null) return;
                    context.ProductMakes.AddRange(productMakes);
                    await context.SaveChangesAsync();
                }
                if (!context.ProductModels.Any())
                {
                    var productModelsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/productmodels.json");
                    var productModels = JsonSerializer.Deserialize<List<ProductModel>>(productModelsData);
                    if (productModels == null) return;
                    context.ProductModels.AddRange(productModels);
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products == null) return;
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync(); // Save to get generated Ids
                }
                
        }
}
