namespace Core.Entities;

public class Product : BaseEntity
{
  
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public required string PictureUrl { get; set; }
    public string Type { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductBrand { get; set; }
    public int ProductBrandId { get; set; }    
    public int QuantityInStock { get; set; }
}
