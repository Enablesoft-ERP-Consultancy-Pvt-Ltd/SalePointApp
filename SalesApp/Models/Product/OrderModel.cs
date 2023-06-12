using System;
using System.Collections.Generic;
using System.Reflection;

namespace SalesApp.Models.Product
{


    public class BaseEntity
    {
        public BaseEntity()
        {
            if (GetType().IsSubclassOf(typeof(BaseEntity)))
            {
                var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    // Get only string properties
                    if (property.PropertyType != typeof(string))
                    {
                        continue;
                    }

                    if (!property.CanWrite || !property.CanRead)
                    {
                        continue;
                    }

                    if (property.GetGetMethod(false) == null)
                    {
                        continue;
                    }
                    if (property.GetSetMethod(false) == null)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty((string)property.GetValue(this, null)))
                    {
                        property.SetValue(this, string.Empty, null);
                    }
                }
            }
        }
    }


    public class BillModel
    {

        public long OrderId { get; set; }

        public long BillId { get; set; }
    }

    public class OrderModel
    {
        public int OrderId { get; set; }
        public DateTime SaleDate { get; set; }
        public string TransactionId { get; set; }
        public int DelieveryType { get; set; }
        public int PortType { get; set; }
        public string Description { get; set; }

        public int SaleStatus { get; set; }
        public int SessionYear { get; set; }
        public List<OrderItemModel> ItemList { get; set; }
        public int Unit { get; set; } //Remove
        public int MirrorId { get; set; }//Remove
        public int DisCountPer { get; set; } //Remove


        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }



    }


    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string TransId { get; set; }
        public int FinishedId { get; set; }
        public string StockId { get; set; }
        public int OrderType { get; set; }
        public string OrderTypePrefix { get; set; }
        public int SalesType { get; set; }
        public int Quantity { get; set; }
        public int CurrencyType { get; set; }
        public double Price { get; set; }
        public double PriceINR { get; set; }
        public double ConversionRate { get; set; }
        public int Unit { get; set; }
        public int ItemType { get; set; }
        public string ItemDescription { get; set; }
        public int SessionYear { get; set; }
        public string HsnCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

        public string Source { get; set; }
        public short PackId { get; set; }



    }



    public class OrderPaymentModel
    {
        public int OrderId { get; set; }
        public int PaymentMode { get; set; }
        public int CardType { get; set; }
        public double Amount { get; set; }
        public double IGST { get; set; }
        public double GST { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Currency { get; set; }
        public int PaylaterStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

    }



    public class CancelOrderModel
    {
        public int OrderId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int PackingDetailId { get; set; }
        public int BillId { get; set; }
        public int SaleStatus { get; set; }
        public DateTime CreatedOn { get; set; }

    }

    public class WishItemModel
    {
        public int WishId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public short IsActive { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    public class WishModel : BaseEntity
    {
        public int WishId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public string SizeInch { get; set; }
        public string PrimePhoto { get; set; }
        public string ProductCode { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string QualityName { get; set; }
        public string DesignName { get; set; }
        public string ColorName { get; set; }
        public string ShadeColorName { get; set; }
        public string ShapeName { get; set; }

    }

}