using System.Data.Entity;

namespace OrderManagement.Data.Models
{
    public class OrderManagementContext : DbContext
    {
        public OrderManagementContext() : base("name=SiteSqlServer")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<OrderManagementContext>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}