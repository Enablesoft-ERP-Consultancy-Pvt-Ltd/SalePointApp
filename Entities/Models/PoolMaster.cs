using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PoolMaster
    {
        public PoolMaster()
        {
            MirrorDetails = new HashSet<MirrorDetails>();
        }

        public long Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string poolcolor { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<MirrorDetails> MirrorDetails { get; set; }
    }
}
