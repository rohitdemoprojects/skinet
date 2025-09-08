using Core.Entities;
namespace Core.Interfaces{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? type, string? brand, string? sort);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IReadOnlyList<string> > GetBrandsAsync();
         Task<IReadOnlyList<string> > GetTypesAsync();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<bool> SaveChangesAsync();
        bool ProductExists(int id); // Add this line
    }
}