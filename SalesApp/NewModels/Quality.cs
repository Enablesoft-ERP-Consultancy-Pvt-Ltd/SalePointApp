using System;
using System.Collections.Generic;

namespace SalesApp.NewModels
{
    public partial class Quality
    {
        public int? QualityId { get; set; }
        public string QualityName { get; set; }
        public int? ItemId { get; set; }
        public int? Userid { get; set; }
        public int? MasterCompanyid { get; set; }
        public double? Loss { get; set; }
        public string Hscode { get; set; }
        public string Instruction { get; set; }
        public string Remark { get; set; }
        public string QualityCode { get; set; }
        public double? MaterialRate { get; set; }
    }
}
