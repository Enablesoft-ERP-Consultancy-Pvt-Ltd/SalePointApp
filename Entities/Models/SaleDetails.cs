using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class SaleDetails
    {
        public long SaleId { get; set; }
        public decimal? SaleAmount { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? PaidByCash { get; set; }
        public decimal? PaidByCard { get; set; }
        public decimal? PaidInStore { get; set; }
        public decimal? PayLater { get; set; }
        public decimal? HdchargeAmount { get; set; }
        public double? CardChargePercentage { get; set; }
        public double? Gst { get; set; }
        public double? Igst { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }
    }
}
