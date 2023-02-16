using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEERP.Models
{
    public partial class PayMode
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
