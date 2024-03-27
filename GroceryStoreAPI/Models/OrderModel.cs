using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
  public class OrderModel
  {
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Contact { get; set; }
    public int TotalPrice { get; set; }
    public int UserId { get; set; }
  }
}
