using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using OrderManagement.Data.Models;
using System.Linq;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class OrderController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void SubmitOrder(Order order)
        {
            try
            {
                var creditCardController = new CreditCardController();
                var db = new OrderManagementContext();

                var ignoreProducts = order.LineItems
                    .Where(li => !ProductController.ProductExists(li.Product.ProductId, li.Quantity))
                    .Select(li => li);

                order.LineItems = (ICollection<OrderItem>) order.LineItems.Except(ignoreProducts);
                
                var chargeResponse = creditCardController.ChargePayment(order.Customer.CreditCardNumber, order.GetTotal());
                if (chargeResponse)
                {
                    foreach (var lineItem in order.LineItems)
                    {
                        ProductController.RemoveProductFromShelf(lineItem.Product, lineItem.Quantity);
                    }
                    EmailController.SendEmail("noreply@shipping.com", order.Customer.EmailAddress,
                        "Order has been shipped.", "This is a body");
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error("Error in Submit Order - ", ex);
            }
        }
    }
}
