using System;

namespace SalesApp.ViewModel
{
    public class ReportVM
    {
        public int srno { get; set; }
        public Int64 BillId { get; set; }
        public Int64 orderid { get; set; }
        public Int64 itemid { get; set; }
        public Int64 paymentid { get; set; }
        public decimal? SaleValue { get; set; }
        public int payment { get; set; }
        public decimal? payamount { get; set; }
        public Decimal? discount { get; set; }
        public int unit { get; set; }
        public string prefix { get; set; }
        public int? paymode { get; set; }
        public DateTime fromdate { get; set; }
      
        public DateTime Todate { get; set; }
        public DateTime? saledate { get; set; }
        public string invno { get; set; }
        public string stockno { get; set; }
        public string itemdesc { get; set; }

        public string size { get; set; }
        public int qty { get; set; }
        public decimal? purchasecost { get; set; }
        public int itemgst { get; set; }
        public int salegst { get; set; }
        public decimal? netpurchasecost { get; set; }
        public decimal? basicamount { get; set; }
        //public decimal? amountpaid { get; set; }
        public decimal? actualbasicamount { get; set; }

    }
}
