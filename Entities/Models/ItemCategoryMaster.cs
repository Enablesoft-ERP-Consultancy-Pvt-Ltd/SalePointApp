using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class ItemCategoryMaster
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public int? Userid { get; set; }
        public int? priortiy { get; set; }
        public int? MasterCompanyid { get; set; }
        public string Hscode { get; set; }
    }
}
