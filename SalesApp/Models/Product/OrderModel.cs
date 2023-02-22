using System;
using System.Collections.Generic;

namespace SalesApp.Models.Product
{
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
        public int TransId { get; set; }
        public int FinishedId { get; set; }
        public string StockId { get; set; }
        public int OrderType { get; set; }
        public string OrderTypePrefix { get; set; }
        public int SalesType { get; set; }
        public int Qty { get; set; }
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

        public string PackSource { get; set; }
        



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













}
