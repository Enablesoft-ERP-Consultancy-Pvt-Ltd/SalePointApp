using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class MirrorList
    {
        public MirrorList()
        {
            OrderMaster = new HashSet<OrderMaster>();
        }

        public long Id { get; set; }
        public long MirrorId { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
        public int? unitid { get; set; }

        public virtual ICollection<OrderMaster> OrderMaster { get; set; }
    }
}
