namespace Infrastructure.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(p => p.ProductBrand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(p => p.Type).Distinct().ToListAsync();
    }
    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? type, string? brand, string? sort)
    {
        var query = context.Products.AsQueryable();
        if(!string.IsNullOrEmpty(brand))      
            query = query.Where(p => p.ProductBrand == brand);
       
        if(!string.IsNullOrEmpty(type))
            query = query.Where(p => p.Type == type);

    
            query = sort switch
            {
                "priceasc" => query.OrderBy(p => p.Price),
                "pricedesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };
       
       
       
        return await query.ToListAsync();
    }

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
    
    public bool ProductExists(int id)
    {
        return context.Products.Any(e => e.Id == id);
    }
}
