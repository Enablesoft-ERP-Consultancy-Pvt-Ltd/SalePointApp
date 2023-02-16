using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CommissionDetails
    {
        public CommissionDetails()
        {
            PayDetails = new HashSet<PayDetails>();
        }

        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public long? MirrorId { get; set; }
        public int? AgentId { get; set; }
        public int? unitid { get; set; }
        public long? SaleId { get; set; }
        public long? PoolId { get; set; }
        public decimal? Pecentage { get; set; }
        public decimal? Amount { get; set; }
        public decimal? tbAmount { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PayDetails> PayDetails { get; set; }
    }
}
