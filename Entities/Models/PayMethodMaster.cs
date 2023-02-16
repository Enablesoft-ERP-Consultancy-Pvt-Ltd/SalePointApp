using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PayMethodMaster
    {
        public int Id { get; set; }
        public string PayName { get; set; }
        public string PayDesc { get; set; }
        public string PayCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
    }
}
