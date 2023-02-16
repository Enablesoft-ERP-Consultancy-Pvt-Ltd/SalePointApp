using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Transactions
    {
        public long? Id { get; set; }
        public long? Value { get; set; }
        public string Source { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Createdby { get; set; }
        public int? Updatedby { get; set; }
        public bool? Isactive { get; set; }
    }
}
