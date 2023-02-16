using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CurrencyMaster
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public decimal? Rate { get; set; }
        public int? Priority { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
    }
}
