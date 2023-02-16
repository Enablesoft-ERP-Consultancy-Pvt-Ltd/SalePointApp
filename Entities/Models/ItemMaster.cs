using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class ItemMaster
    {
        public int ItemId { get; set; }
        public int? CategoryId { get; set; }
        public int? UnitTypeId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int? Userid { get; set; }
        public int? MasterCompanyid { get; set; }
        public int? FlagFixWeight { get; set; }
        public int? ItemType { get; set; }
        public int? Katiwithexportsize { get; set; }
        public byte? Itemstatusbypassonfolio { get; set; }
        public byte? Ismultipleconstruction { get; set; }
    }
}
