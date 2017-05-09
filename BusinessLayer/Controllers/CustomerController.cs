using System.Linq;
using OrderManagement.Data.Models;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class CustomerController
    {
        public void AddCustomer(Customer customer)
        {
            var db = new OrderManagementContext();
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public Customer GetCustomer(int customerId)
        {
            var db = new OrderManagementContext();
            return db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }
    }
}
