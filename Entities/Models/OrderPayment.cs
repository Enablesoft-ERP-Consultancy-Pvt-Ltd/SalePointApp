using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class OrderPayment
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long BillId { get; set; }
        public int? PayMode { get; set; }
        public int? CardType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmoutHd { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Gst { get; set; }
        public DateTime? PayDate { get; set; }
        public int? CurrencyType { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        public DateTime? paylaterdate { get; set; }
        public bool? paylaterstatus { get; set; }
        public bool? IsActive { get; set; }

        public virtual OrderMaster Order { get; set; }
    }
}
