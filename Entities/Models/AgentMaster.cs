using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class AgentMaster
    {
        public AgentMaster()
        {
            AgentUser = new HashSet<AgentUser>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<AgentUser> AgentUser { get; set; }
    }
}
