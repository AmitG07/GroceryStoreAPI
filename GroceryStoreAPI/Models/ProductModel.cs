using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
  public class ProductModel
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Image { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Discount { get; set; }
    public string Specification { get; set; } = string.Empty;
  }
}
