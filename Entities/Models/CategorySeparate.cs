using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CategorySeparate
    {
        public int SrNo { get; set; }
        public int Id { get; set; }
        public int Categoryid { get; set; }
        public int? Userid { get; set; }
        public int? MasterCompanyid { get; set; }
    }
}
