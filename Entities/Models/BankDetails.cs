using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class BankDetails
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int? BankId { get; set; }
        public string Description { get; set; }
        public string AccountNo { get; set; }
        public string ifsc { get; set; }
        public string gstin { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
        public virtual BankMaster Bank { get; set; }
    }
}
