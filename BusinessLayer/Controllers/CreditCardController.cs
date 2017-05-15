using System.Configuration;
using System.Reflection;
using log4net;
using OrderManagement.BusinessLayer.Interfaces;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class CreditCardController : ICreditCardProcessor
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public bool ChargePayment(string creditCardNumber, double amount)
        {
            Log.Info($"Your payment for the credit card numner {creditCardNumber} to the amount of {amount} has been processed.");
            return true;
        }
    }
}
