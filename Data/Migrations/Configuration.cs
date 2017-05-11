using System;
using System.Data.Entity.Migrations;

namespace OrderManagement.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.OrderManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.OrderManagementContext context)
        {
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product {ProductName = @"Milk", Price = GetRandomNumber(1, 5), Quantity = 10},
                new Models.Product {ProductName = @"Bread", Price = GetRandomNumber(3, 6), Quantity = 10},
                new Models.Product {ProductName = @"Yogurt", Price = GetRandomNumber(5, 10), Quantity = 10},
                new Models.Product {ProductName = @"Steak", Price = GetRandomNumber(10, 20), Quantity = 10},
                new Models.Product {ProductName = @"Eggs", Price = GetRandomNumber(2, 4), Quantity = 10},
                new Models.Product {ProductName = @"Tomatoes", Price = GetRandomNumber(2, 4), Quantity = 10},
                new Models.Product {ProductName = @"Avocado", Price = GetRandomNumber(3, 7), Quantity = 10},
                new Models.Product {ProductName = @"Butter", Price = GetRandomNumber(3, 7), Quantity = 10},
                new Models.Product {ProductName = @"Cheddar Cheese", Price = GetRandomNumber(5, 10), Quantity = 10},
                new Models.Product {ProductName = @"Brie Cheese", Price = GetRandomNumber(5, 10), Quantity = 10}
            );
            context.SaveChanges();
        }

        private static double GetRandomNumber(double min, double max)
        {
            var random = new Random();
            return Math.Round(random.NextDouble() * (max - min) + min, 2);
        }
    }
}
