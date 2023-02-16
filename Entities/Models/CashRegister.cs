using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CashRegister
    {
        public long Id { get; set; }
        public int? ExpensTypeId { get; set; }
        public int? TypeId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CashDate { get; set; }
        public string Narration { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UnitId { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
