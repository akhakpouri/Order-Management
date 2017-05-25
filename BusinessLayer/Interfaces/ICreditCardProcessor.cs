namespace OrderManagement.BusinessLayer.Interfaces
{
    public interface ICreditCardProcessor
    {
        bool ChargePayment(string creditCardNumber, double amount);
    }
}
