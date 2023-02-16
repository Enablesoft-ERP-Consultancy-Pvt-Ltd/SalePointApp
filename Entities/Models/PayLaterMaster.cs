using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PayLaterMaster
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeDesc { get; set; }
        public string TypeCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
    }
}
