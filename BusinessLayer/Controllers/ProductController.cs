using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using OrderManagement.Data.Models;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class ProductController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IEnumerable<Product> GetAllAvailableProducts()
        {
            var db = new OrderManagementContext();
            return db.Products.Where(p => p.Quantity > 0);
        }

        public static IEnumerable<Product> GetProductsByName(string name)
        {
            var db = new OrderManagementContext();
            return db.Products.Where(p => p.Quantity > 0 && p.ProductName.Contains(name));
        }

        public static bool ProductExists(int productId, int quantity)
        {
            var db = new OrderManagementContext();
            var productExists = db.Products.Count(p => p.Quantity > quantity && p.ProductId == productId) > 0;
            if (productExists)
                return true;
            Log.Error($"Product id {productId} was requested, but there is not enought items in the inventory");
            return false;
        }

        public static void RemoveProductFromShelf(Product product, int quantity)
        {
            if (product.Quantity <= 0) return;
            if (product.Quantity >= quantity)
                product.Quantity -= quantity;
            else
                product.Quantity = 0;
        }
    }
}
