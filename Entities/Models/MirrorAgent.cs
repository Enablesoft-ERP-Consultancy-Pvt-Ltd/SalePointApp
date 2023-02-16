using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class MirrorAgent
    {
        public long Id { get; set; }
        public long? MirrorId { get; set; }
        public int? AgentId { get; set; }
        public decimal? ParchiAmount { get; set; }
        public bool? IsParchi { get; set; }
        public bool? IsDemo { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual MirrorDetails Mirror { get; set; }
    }
}
