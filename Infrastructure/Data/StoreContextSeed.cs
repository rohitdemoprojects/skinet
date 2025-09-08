namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Data", "SeedData", "products.json");
                var productsData = File.ReadAllText(path);
                var products = System.Text.Json.JsonSerializer.Deserialize<List<Core.Entities.Product>>(productsData);
                if (products != null && products.Count > 0)
                {
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding error: {ex.Message}");
            }
        }
    }
}