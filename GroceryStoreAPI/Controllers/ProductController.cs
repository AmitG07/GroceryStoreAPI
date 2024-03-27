using GroceryStoreAPI.Data;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : Controller
  {

    private readonly GroceryDbContext _groceryDbContext;
    public ProductController(GroceryDbContext groceryDbContext)
    {
      _groceryDbContext = groceryDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
      var allProductsList = await _groceryDbContext.Products.ToListAsync();
      return Ok(allProductsList);
    }


    [HttpPost]
    public async Task<ActionResult<List<ProductModel>>> AddProduct( ProductModel newProduct)
    {
       _groceryDbContext.Products.Add(newProduct);
      await _groceryDbContext.SaveChangesAsync(); 

      return Ok(await _groceryDbContext.Products.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<ProductModel>>> EditProduct(ProductModel product)
    {
      var currProduct = await _groceryDbContext.Products.FindAsync(product.Id);
      if (currProduct == null)
      {
        return BadRequest("Product with that particular Id not found.");
      }
      else
      {
        currProduct.Name = product.Name;
        currProduct.Description = product.Description;
        currProduct.Category = product.Category;
        currProduct.Quantity = product.Quantity;
        currProduct.Image = product.Image;
        currProduct.Price = product.Price;
        currProduct.Discount = product.Discount;
        currProduct.Specification = product.Specification;

        await _groceryDbContext.SaveChangesAsync();
        return Ok(await _groceryDbContext.Products.ToListAsync());
      }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ProductModel>> GetProductById(int id)
    {
      var product = await _groceryDbContext.Products.FindAsync(id);
      if (product== null)
      {
        return BadRequest("this product is not available");
      }
      await _groceryDbContext.SaveChangesAsync();
      return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<ProductModel>>> DeleteProductById(int id)
    {
      var product = await _groceryDbContext.Products.FindAsync(id);
      if (product== null)
      {
        return BadRequest("Product is not available");
      }
      _groceryDbContext.Products.Remove(product);
      await _groceryDbContext.SaveChangesAsync();
      return Ok(await _groceryDbContext.Products.ToListAsync());
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ProductModel>>> SearchProducts(string query)
    {
      if (string.IsNullOrWhiteSpace(query))
      {
        return BadRequest("Nothing Found!");
      }
      else
      {
        var products =_groceryDbContext.Products.Where(p => p.Name.Contains(query) || p.Description.Contains(query)|| p.Category.Contains(query)).ToListAsync();
        return Ok(await products);
      }
    }

    [HttpGet("popular-products")]
    public async Task<ActionResult<List<ProductModel>>> PopularProducts()
    {
      var products = await _groceryDbContext.Products.OrderByDescending(result => result.Quantity).Take(5).ToListAsync();
      if (products.Count == 0)
      {
        return NotFound();
      }
      return Ok(products);
    }

    // api fot category dropdown 
    [HttpGet("category/{value}")]
    public async Task<IActionResult> GetProductsByCategory(string value)
    {
      if (value == "all")
      {
        var products = await _groceryDbContext.Products.ToListAsync();
        return Ok(products);
      }
      else
      {
        var products = await _groceryDbContext.Products.Where(p => p.Category == value).ToListAsync();
        return Ok(products);
      }
    }


  }
}
