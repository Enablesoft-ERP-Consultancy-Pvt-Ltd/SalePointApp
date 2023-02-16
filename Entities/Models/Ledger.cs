using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Ledger
    {
        public long Id { get; set; }
        public long? Payid { get; set; }
        public decimal? Amount { get; set; }
        public int? Agentid { get; set; }
        public int? Paymode { get; set; }
        public string Paydesc { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Updatedby { get; set; }
        public bool? Isactive { get; set; }
    }
}
