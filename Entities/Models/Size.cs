using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Size
    {
        public int SizeId { get; set; }
        public int UnitId { get; set; }
        public int Shapeid { get; set; }
        public decimal? WidthFt { get; set; }
        public decimal? LengthFt { get; set; }
        public double? HeightFt { get; set; }
        public double? WidthMtr { get; set; }
        public double? LengthMtr { get; set; }
        public double? HeightMtr { get; set; }
        public double? AreaFt { get; set; }
        public double? VolumeFt { get; set; }
        public double? AreaMtr { get; set; }
        public double? VolumeMtr { get; set; }
        public string SizeFt { get; set; }
        public string SizeMtr { get; set; }
        public int? Userid { get; set; }
        public int? MasterCompanyid { get; set; }
        public double? WidthInch { get; set; }
        public double? LengthInch { get; set; }
        public double? HeightInch { get; set; }
        public double? AreaInch { get; set; }
        public double? VolumeInch { get; set; }
        public string SizeInch { get; set; }
        public decimal? ProdLengthFt { get; set; }
        public decimal? ProdWidthFt { get; set; }
        public double? ProdLengthMtr { get; set; }
        public double? ProdWidthMtr { get; set; }
        public string ProdSizeFt { get; set; }
        public string ProdSizeMtr { get; set; }
        public double? ProdAreaFt { get; set; }
        public double? ProdAreaMtr { get; set; }
        public decimal? Actualfullareasqyd { get; set; }
    }
}
