using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class SpecialAdditionDetails
    {
        public long Id { get; set; }
        public long? OrderItemId { get; set; }
        public int? SpecialAdditionId { get; set; }
        public string SpecialAdditionDesc { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
