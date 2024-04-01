using GroceryStoreAPI.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data
{
  public class GroceryDbContext : DbContext
  {
    public GroceryDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CartModel> Carts { get; set; }
    public DbSet<UserModel> UsersData { get; set; }
    public DbSet<AdminModel> Admins { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
  }
}
