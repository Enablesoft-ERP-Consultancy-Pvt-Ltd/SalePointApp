using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class LoginRolesVM
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescripton { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<RoleclaimVM> Roleclaim { get; set; }



    }
}
