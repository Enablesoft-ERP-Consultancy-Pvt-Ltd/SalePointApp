using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CarpetNumber
    {
        public int StockNo { get; set; }
        public int item_finished_id { get; set; }
        public int TypeId { get; set; }
        public int? Pack { get; set; }
        public int? OrderId { get; set; }
        public string TStockNo { get; set; }
        public string Prefix { get; set; }
        public int Postfix { get; set; }
        public int Companyid { get; set; }
        public int ProcessRecId { get; set; }
        public int? ProcessRecDetailId { get; set; }
        public DateTime? RecDate { get; set; }
        public DateTime? PackDate { get; set; }
        public int? CurrentProStatus { get; set; }
        public int? IssRecStatus { get; set; }
        public int? PackingId { get; set; }
        public int? PackingDetailId { get; set; }
        public int? StockUnitid { get; set; }
        public byte? Confirm { get; set; }
        public int? Locationid { get; set; }
        public decimal? Price { get; set; }
        //public string OldStockNo { get; set; }
        //public int? Godownid { get; set; }
        //public int? Ismobsend { get; set; }
        public string PackSource { get; set; }
        //public bool? Damaged { get; set; }
        //public string Comments { get; set; }
        //public DateTime? Confirmdate { get; set; }
        //public int? Userid { get; set; }
    }
}
