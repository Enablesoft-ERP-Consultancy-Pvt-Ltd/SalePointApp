using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Directstockpack
    {
        public long DId { get; set; }
        public int? Id { get; set; }
        public long? Stockno { get; set; }
        public int? Userid { get; set; }
        public string Remark { get; set; }
        public DateTime? Dateadded { get; set; }
    }
}
