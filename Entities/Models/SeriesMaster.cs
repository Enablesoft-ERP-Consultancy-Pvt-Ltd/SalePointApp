using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class SeriesMaster
    {
        public SeriesMaster()
        {
            MirrorDetails = new HashSet<MirrorDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<MirrorDetails> MirrorDetails { get; set; }
    }
}
