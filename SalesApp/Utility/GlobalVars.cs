using Microsoft.CodeAnalysis.Options;
using System.ComponentModel;

namespace SalesApp.Utility
{
    public enum ShoppingType
    {
        SalesPoint = 0,
        Website,
    }

    public enum SalesStatus
    {
        Ordered = 0,
        Purchased,
    }

    public enum CardType
    {
        Amex = 1,
        Visa,
        Mastercard,
        Diners, Maestro,
        JCB,
        Discover,
        RuPay,
        Other,
    }



    public enum PaymentMode
    {
        CreditCard = 1,
        Cash,
        PayLater,
        DebitCard,
        BankTransfer,
    }








    public enum Status
    {
        [Description("In-Active")]
        InActive = 0,
        [Description("Active")]
        Active = 1,
        [Description("Suspended")]
        Suspended = 2,
        [Description("Deleted")]
        Deleted = 3,


    }


}

