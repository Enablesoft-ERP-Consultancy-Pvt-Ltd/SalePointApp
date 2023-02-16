using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PayDetails
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public int? AgentId { get; set; }
        //public int? fid { get; set; }
        public int? FavourOfId { get; set; }
        public int? unitid { get; set; }
        public long? MirrorId { get; set; }
        public long? PoolId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PayCash { get; set; }
        public decimal? PayCheck { get; set; }
        public decimal? PayBT { get; set; }
        public decimal? tds { get; set; }
        public long? CommId { get; set; }

        public int? payment_by { get; set; }
        public string infavourof { get; set; }
        public string checkorbtno { get; set; }

        public int? pay_mode { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? pay_check_datetime { get; set; }
        public bool? pay_check_status { get; set; }
        public int? pay_check_by { get; set; }
        public DateTime? pay_bt_datetime { get; set; }
        public bool? pay_bt_status { get; set; }
        public int? pay_bt_by { get; set; }
        public virtual AgentUser Agent { get; set; }
        public virtual CommissionDetails Comm { get; set; }
        public virtual MirrorDetails Mirror { get; set; }
      
        public long? bankmasterid { get; set; }
        public long? bankdetailid { get; set; }

    }
}
