using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class LoginRoles
    {
        public LoginRoles()
        {
            Roleclaim = new HashSet<Roleclaim>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Roleclaim> Roleclaim { get; set; }
    }
}
