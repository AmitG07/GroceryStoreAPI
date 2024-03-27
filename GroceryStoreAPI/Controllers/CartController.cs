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

  public class CartController : Controller
  {
    private readonly GroceryDbContext _groceryDbContext;
    public CartController(GroceryDbContext groceryDbContext)
    {
      _groceryDbContext = groceryDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<List<CartModel>>> AddToCart(CartModel cartData)
    {
      _groceryDbContext.Carts.Add(cartData);
      await _groceryDbContext.SaveChangesAsync();

      return Ok(await _groceryDbContext.Carts.ToListAsync());
    }

    [HttpGet("userId")]
    public async Task<ActionResult<List<ProductModel>>> GetCartList(int userId)
    {
      var cart = await _groceryDbContext.Carts.ToListAsync();
      var userCart = cart.Where(i => i.UserId == userId).ToList();
      if (userCart == null)
      {
        return BadRequest("No product found in cart!");
      }
      return Ok(userCart);
    }

    [HttpDelete("remove")]
    public async Task<ActionResult<CartModel>> DeleteFromCart(int cartId)
    {
      var cartData = await _groceryDbContext.Carts.FindAsync(cartId);
      if (cartData == null)
      {
        return BadRequest("Item not Found!");
      }
      _groceryDbContext.Carts.Remove(cartData);
      await _groceryDbContext.SaveChangesAsync();
      return Ok(await _groceryDbContext.Carts.ToListAsync());
    }

    [HttpGet("current-cart")]
    public async Task<ActionResult<List<CartModel>>> CurrentCartDetails(int userId)
    {
      var cartData = await _groceryDbContext.Carts.ToListAsync();
      var userCartList = cartData.Where(cart => cart.UserId == userId).ToList();
      await _groceryDbContext.SaveChangesAsync();
      if (userCartList == null)
      {
        return BadRequest("No orders found for this user!");
      }
      return Ok(userCartList);
    }

  }
}
