using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Utility;

namespace SalesApp.ViewModel
{
    public class OrderPaymentVM
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public int? PayMode { get; set; }
        public int? CardType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmoutHd { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Gst { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        public bool? IsActive { get; set; }

        //public virtual OrderMaster Order { get; set; }

    }
}
