using System.ComponentModel;

namespace SalesApp.Common
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

