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
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : Controller
  {
    private readonly GroceryDbContext _groceryDbContext;
    public AdminController(GroceryDbContext groceryDbContext)
    {
      _groceryDbContext = groceryDbContext;
    }


    [HttpGet("admin-login")]
    public async Task<ActionResult<AdminModel>> AdminLogin(string email, string password)
    {
      var allAdmins = await _groceryDbContext.Admins.ToListAsync();
      var currentAdmin = allAdmins.FirstOrDefault(i => i.Email == email && i.Password == password);
      if (currentAdmin == null)
      {
        NotFound("Admin does not exit");
      }
      return Ok(currentAdmin);
    }

    [HttpPost("admin-signup")]
    public async Task<ActionResult<AdminModel>> AdminSignUp(AdminModel adminData)
    {
      var admins = await _groceryDbContext.Admins.ToListAsync();
      if (admins == null)
      {
        return BadRequest("Users are not present");
      }
      _groceryDbContext.Admins.Add(adminData);
      await _groceryDbContext.SaveChangesAsync();
      var currAdmin = await _groceryDbContext.Admins.FindAsync(adminData.Id);
      return Ok(currAdmin);
    }
  }
}
