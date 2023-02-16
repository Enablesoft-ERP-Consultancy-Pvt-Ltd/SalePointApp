using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Shape
    {
        public int ShapeId { get; set; }
        public string ShapeName { get; set; }
        public int? Userid { get; set; }
        public int? MasterCompanyid { get; set; }
        public int? Type { get; set; }
    }
}
