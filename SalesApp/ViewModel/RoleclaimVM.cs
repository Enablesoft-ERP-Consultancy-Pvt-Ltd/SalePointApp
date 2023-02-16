using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class RoleclaimVM
    {
        public int ClaimId { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual LoginRolesVM Role { get; set; }
        public virtual UserLoginVM User { get; set; }
    }
}
