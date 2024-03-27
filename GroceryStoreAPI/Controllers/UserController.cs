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
  public class UserController : Controller
  {
    private readonly GroceryDbContext _groceryDbContext;
    public UserController(GroceryDbContext groceryDbContext)
    {
      _groceryDbContext = groceryDbContext;
    }
    [HttpGet("user-login")]
    public async Task<ActionResult<UserModel>> UserLogin(string email, string password)
    {
      var allUsers = await _groceryDbContext.Users.ToListAsync();
      var currentUser = allUsers.FirstOrDefault(i => i.Email == email && i.Password == password);
      if (currentUser == null)
      {
        NotFound("User does not exit");
      }
      return Ok(currentUser);
    }

    [HttpPost("user-signup")]
    public async Task<ActionResult<AdminModel>> UserSignUp(UserModel userData)
    {
      var  users=await _groceryDbContext.Users.ToListAsync();
      if (users == null)
      {
        return BadRequest("Users are not present");
      }
      _groceryDbContext.Users.Add(userData);
      await _groceryDbContext.SaveChangesAsync();
      var currUser = await _groceryDbContext.Users.FindAsync(userData.Id);
      return Ok(currUser);
    }
  }
}
