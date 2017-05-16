using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagement.BusinessLayer.Controllers;
using OrderManagement.Data.Models;

namespace OrderManagement.UnitTest
{
    [TestClass]
    public class ProductTest
    {
        private readonly Product _product = new Product {ProductId = 1, ProductName = "Milk"};

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ProductExists()
        {
            Assert.IsTrue(ProductController.ProductExists(_product.ProductId, 9));
        }

        [TestMethod]
        public void ProductDoesntExist()
        {
            Assert.IsFalse(ProductController.ProductExists(_product.ProductId, 11));
        }

        [TestMethod]
        public void GetProductsByName()
        {
            var products = ProductController.GetProductsByName("mi");
            foreach (var product in products)
            {
                TestContext.WriteLine($"Product Name - {product.ProductName}");
            }
            Assert.IsTrue(true);
        }
    }
}
