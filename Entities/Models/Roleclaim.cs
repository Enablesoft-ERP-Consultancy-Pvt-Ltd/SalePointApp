using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Roleclaim
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual LoginRoles Role { get; set; }
        public virtual UserLogin User { get; set; }
    }
}
