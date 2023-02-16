using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class MirrorDetails
    {
        public MirrorDetails()
        {
            MirrorAgent = new HashSet<MirrorAgent>();
            PayDetails = new HashSet<PayDetails>();
        }

        public long Id { get; set; }
        public long mrid { get; set; }
        public DateTime? Date { get; set; }
        public int? Pax { get; set; }
        public int? CountryId { get; set; }
        public int? LanguageId { get; set; }
        public long? PoolId { get; set; }
        public int? SeriesId { get; set; }
        public int? StatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? unitid { get; set; }
        public bool? IsActive { get; set; }
        public Decimal? CardCharges { get; set; }
        public Decimal? GstCharges { get; set; }
        public Decimal? HdAmount { get; set; }


        public virtual CountriesMaster Country { get; set; }
        public virtual UserLogin CreatedByNavigation { get; set; }
        public virtual LanguagesMaster Language { get; set; }
        public virtual PoolMaster Pool { get; set; }
        public virtual SeriesMaster Series { get; set; }
        public virtual ICollection<MirrorAgent> MirrorAgent { get; set; }
        public virtual ICollection<PayDetails> PayDetails { get; set; }
    }
}
