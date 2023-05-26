using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Session
    {
        public int? Year { get; set; }
        public string Session1 { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
