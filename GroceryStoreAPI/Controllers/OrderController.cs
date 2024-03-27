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
  public class OrderController : Controller
  {
    private readonly GroceryDbContext _groceryDbContext;
    public OrderController(GroceryDbContext groceryDbContext)
    {
      _groceryDbContext = groceryDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<OrderModel>> PlaceOrder(OrderModel orderData)
    {
      if (orderData == null)
      {
        return BadRequest("NOt able to place such order");
      }
      await _groceryDbContext.Orders.AddAsync(orderData);
      await _groceryDbContext.SaveChangesAsync();
      return Ok(orderData);
    }

    [HttpDelete("delete-order")]
    public async Task<ActionResult<List<OrderModel>>> DeleteOrder(int orderId)
    {
      var order = await _groceryDbContext.Orders.FindAsync(orderId);
      if (order == null)
      {
        return BadRequest("this order is not present in database");
      }
      _groceryDbContext.Orders.Remove(order);
      await _groceryDbContext.SaveChangesAsync();
      return Ok(await _groceryDbContext.Orders.ToListAsync());
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<List<OrderModel>>> GetOrderById(int id)
    {
      var orders = await _groceryDbContext.Orders.ToListAsync();
      var currOrder = orders.Where(i => i.UserId == id).ToList();
      if (currOrder== null)
      {
        return BadRequest("this order is not avaialable in database");
      }
      return Ok(currOrder);
    }
  }
}
