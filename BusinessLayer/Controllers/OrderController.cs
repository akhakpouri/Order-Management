using System;
using System.Reflection;
using AuthorizeNet.Api.Contracts.V1;
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
                var isCardAuthorized = CreditCardController.AuthorizeCreditCard(order.Customer.CreditCardNumber,
                    order.Customer.ExpirationDate, order.Total);
                var db = new OrderManagementContext();
                if (isCardAuthorized.messages.resultCode == messageTypeEnum.Ok)
                {
                    var chargeResponse = CreditCardController.ChargeCreditCard(order);
                    if (chargeResponse.messages.resultCode != messageTypeEnum.Ok) return;
                    foreach (var lineItem in order.LineItems)
                    {
                        ProductController.RemoveProductFromShelf(lineItem.Product, lineItem.Quantity);
                    }
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
