using System.Configuration;
using System.Linq;
using System.Reflection;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using log4net;
using OrderManagement.Data.Models;

namespace OrderManagement.BusinessLayer.Controllers
{
    public class CreditCardController
    {
        private static readonly string ApiLoginId = ConfigurationManager.AppSettings["ApiLoginId"];
        private static readonly string ApiTransactionKey = ConfigurationManager.AppSettings["ApiTransactionKey"];
        private static readonly string ApiSecretKey = ConfigurationManager.AppSettings["ApiSecretKey"];
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ANetApiResponse AuthorizeCreditCard(string cardNumber, string expirationDate, decimal amount)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardNumber,
                expirationDate = expirationDate
            };

            var paymentType = new paymentType {Item = creditCard};

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(), // authorize only
                amount = amount,
                payment = paymentType
            };
            var request = new createTransactionRequest {transactionRequest = transactionRequest};

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            //validate
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Log.Info("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Log.Info("Response Code: " + response.transactionResponse.responseCode);
                        Log.Info("Message Code: " + response.transactionResponse.messages[0].code);
                        Log.Info("Description: " + response.transactionResponse.messages[0].description);
                        Log.Info("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Log.Warn("Failed Transaction.");
                        if (response.transactionResponse.errors == null) return response;
                        Log.Error("Transaction Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Log.Error("Transaction Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                }
                else
                {
                    Log.Warn("Failed Transaction.");
                    if (response.transactionResponse?.errors != null)
                    {
                        Log.Error("Transaction Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Log.Error("Transaction Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Log.Error("Transaction Error Code: " + response.messages.message[0].code);
                        Log.Error("Transaction Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Log.Error("Null Response.");
            }
            return response;
        }

        public static ANetApiResponse ChargeCreditCard(Order order)
        {
            Log.Info("About to start the credit card transaction");
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType
            {
                name = ApiLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };
            var creditCard = new creditCardType
            {
                cardNumber = order.Customer.CreditCardNumber,
                expirationDate = order.Customer.ExpirationDate,
                cardCode = order.Customer.CardCode
            };
            var billingAddress = new customerAddressType
            {
                firstName = order.Customer.FirstName,
                lastName = order.Customer.LastName,
                address = order.Customer.Address,
                city = order.Customer.City,
                zip = order.Customer.ZipCode
            };
            var paymentType = new paymentType {Item = creditCard};

            // Add line Items
            var lineItems = order.LineItems.Select((li, index) => new lineItemType
            {
                itemId = index.ToString(),
                name = li.Product.ProductName,
                quantity = li.Quantity,
                unitPrice = new decimal(li.Product.Price)
            }).ToArray();

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(), // charge the card

                amount = order.Total,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };

            var request = new createTransactionRequest {transactionRequest = transactionRequest};

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            //validate
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Log.Info("Successfully created transaction with Transaction ID: " +
                                 response.transactionResponse.transId);
                        Log.Info("Response Code: " + response.transactionResponse.responseCode);
                        Log.Info("Message Code: " + response.transactionResponse.messages[0].code);
                        Log.Info("Description: " + response.transactionResponse.messages[0].description);
                        Log.Info("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Log.Warn("Failed Transaction.");
                        if (response.transactionResponse.errors == null) return response;
                        Log.Error("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Log.Error("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                }
                else
                {
                    Log.Warn("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Log.Error("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Log.Error("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Log.Error("Error Code: " + response.messages.message[0].code);
                        Log.Error("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Log.Error("Null Response.");
            }

            return response;
        }
    }
}
