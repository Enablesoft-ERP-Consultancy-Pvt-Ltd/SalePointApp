using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class AgentUser
    {
        public AgentUser()
        {
            AgentContact = new HashSet<AgentContact>();
            BankDetails = new HashSet<BankDetails>();
            FavourMaster = new HashSet<FavourMaster>();
            PayDetails = new HashSet<PayDetails>();
            VehicleDetails = new HashSet<VehicleDetails>();
        }

        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string SeriesId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string GSTIN { get; set; }

        public int? country_id { get; set; }
        public int? contract_id { get; set; }
        public DateTime? ContractStartdate { get; set; }
        public DateTime? ContractEnddate { get; set; }
        public string Panno { get; set; }
        public int? ParcheeId { get; set; }
        public string Status { get; set; }
       
        public string Website { get; set; }
        public string ContractFormalities { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual AgentMaster Type { get; set; }
        public virtual ICollection<AgentContact> AgentContact { get; set; }
        public virtual ICollection<BankDetails> BankDetails { get; set; }
        public virtual ICollection<FavourMaster> FavourMaster { get; set; }
        public virtual ICollection<PayDetails> PayDetails { get; set; }
        public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }
    }
}
