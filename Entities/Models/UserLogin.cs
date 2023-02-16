using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            MirrorDetails = new HashSet<MirrorDetails>();
            Roleclaim = new HashSet<Roleclaim>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string LoginPass { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<MirrorDetails> MirrorDetails { get; set; }
        public virtual ICollection<Roleclaim> Roleclaim { get; set; }
    }
}
