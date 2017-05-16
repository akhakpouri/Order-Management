using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagement.BusinessLayer.Controllers;

namespace OrderManagement.UnitTest
{
    [TestClass]
    public class TestEmail
    {
        [TestMethod]
        public void Test()
        {
            var emailSent = EmailController.SendEmail("test@testshipping.com", "user@test.com", "This is a test subject", "Test body");
            Assert.IsTrue(emailSent);
        }
    }
}
