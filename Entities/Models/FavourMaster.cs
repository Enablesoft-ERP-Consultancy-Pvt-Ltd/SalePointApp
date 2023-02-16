using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class FavourMaster
    {
        public int Id { get; set; }
        public int? Agetid { get; set; }
        public int? BankId { get; set; }
        public string Name { get; set; }
        public string Bankname { get; set; }
        public string Panno { get; set; }
        public string gstin { get; set; }
        public string Accountno { get; set; }
        public string Ifsc { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public virtual AgentUser Aget { get; set; }
    }
}
