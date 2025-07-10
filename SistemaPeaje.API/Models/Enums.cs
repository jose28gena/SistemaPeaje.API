namespace SistemaPeaje.API.Models
{
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard = 2,
        DebitCard = 3,
        ElectronicTag = 4,
        MobilePayment = 5,
        BankTransfer = 6
    }
    
    public enum TransactionStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Cancelled = 4,
        Refunded = 5
    }
}
