using System.Collections.Generic;
using System.Linq;
using OrderManagement.Data.Models;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class ProductController
    {
        public IEnumerable<Product> GetAllAvailableProducts()
        {
            var db = new OrderManagementContext();
            return db.Products.Where(p => p.Quantity > 0);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var db = new OrderManagementContext();
            return db.Products.Where(p => p.Quantity > 0 && p.ProductName.Contains(name));
        }
    }
}
