using System;
using System.Collections.Generic;

namespace SalesApp.Models.Product
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int SaleDate { get; set; }
        public int TransactionId { get; set; }
        public int DelieveryType { get; set; }
        public int PortType { get; set; }
        public int Description { get; set; }
        public int CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }
        public int SaleStatus { get; set; }
        public int SessionYear { get; set; }
        public List<OrderItemModel> ItemList { get; set; }
        public int Unit { get; set; } //Remove
        public int MirrorId { get; set; }//Remove
        public int DisCountPer { get; set; } //Remove



    }


    public class OrderItemModel
    {
        public int OrderId { get; set; }
        public int TransId { get; set; }
        public int FinishedId { get; set; }
        public int StockId { get; set; }
        public int OrderType { get; set; }
        public int OrderTypeprefix { get; set; }
        public int saleType { get; set; }
        public int Qty { get; set; }
        public int CurrencyType { get; set; }
        public int Price { get; set; }
        public int PriceInr { get; set; }
        public int Conversionrate { get; set; }
        public int Unit { get; set; }
        public int ItemType { get; set; }
        public int CustomSpecialEdition { get; set; }
        public int createddatetime { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }
        public int SessionYear { get; set; }
        public int HsnCode { get; set; }


    }



    public class OrderPaymentModel
    {
        public int TransId { get; set; }
        public int OrderId { get; set; }
        public int PayMode { get; set; }
        public int CardType { get; set; }
        public double Amount { get; set; }
        public double IGST { get; set; }
        public double GST { get; set; }
        public DateTime PayDate { get; set; }
        public int CurrencyType { get; set; }
        public int CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }





    }













}
