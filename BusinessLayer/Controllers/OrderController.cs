using System;
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
                var chargeResponse = creditCardController.ChargePayment(order.Customer.CreditCardNumber, order.GetTotal());


                    
                    
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error("Error in Submit Order - ", ex);
            }
        }
    }
}
