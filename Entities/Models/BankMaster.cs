using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class BankMaster
    {
        public BankMaster()
        {
            BankDetails = new HashSet<BankDetails>();
        }

        public int Id { get; set; }
        //public string accoutId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
       // public string AccountNo { get; set; }
        public string Code { get; set; }
        public string Ifsc { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<BankDetails> BankDetails { get; set; }
    }
}
