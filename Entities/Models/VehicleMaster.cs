using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class VehicleMaster
    {
        public VehicleMaster()
        {
            VehicleDetails = new HashSet<VehicleDetails>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }
    }
}
