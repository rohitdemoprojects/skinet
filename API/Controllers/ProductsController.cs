using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{


   [HttpGet]
   public async Task<ActionResult<IReadOnlyList<Core.Entities.Product>>> GetProducts(string? type, string? brand, string? sort)
   {
       var products = await repo.GetProductsAsync(type, brand, sort);
       return Ok(products);
   }

    [HttpGet("{id}")]
    public async Task<ActionResult<Core.Entities.Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Core.Entities.Product>> CreateProduct(Core.Entities.Product product)
    {
        //_context.Products.Add(product);
        //await _context.SaveChangesAsync();
        repo.AddProduct(product);

        if (repo.SaveChangesAsync().Result)
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        return BadRequest("Failed to create product.");

    }

   [HttpPut("{id}")]
public async Task<IActionResult> UpdateProduct(int id, Core.Entities.Product product)
{
    if (id != product.Id || !ProductExists(id))
    {
        return BadRequest("Product ID mismatch or product does not exist.");
    }

    repo.UpdateProduct(product);

    if (await repo.SaveChangesAsync())
    {
        return NoContent();
    }
    return BadRequest("Failed to update product.");
}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product =await repo.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        repo.DeleteProduct(product);
      if (await repo.SaveChangesAsync())
        {
            return NoContent();
           
        }   
         return BadRequest( "Failed to delete product.");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var brands = await repo.GetBrandsAsync();
        return Ok(brands);
    }
    
     [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var types = await repo.GetTypesAsync();
        return Ok(types);
    }
    
    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }
}