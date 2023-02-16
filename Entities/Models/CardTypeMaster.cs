using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CardTypeMaster
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardDesc { get; set; }
        public string CardCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
    }
}
