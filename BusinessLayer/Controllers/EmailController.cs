using System.Reflection;
using log4net;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class EmailController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendEmail(string from, string to, string subject, string body)
        {
            Log.Info($"An email was sent from {from} to {to} with the subject of {subject}. Body was {body}");
        }
    }
}
