using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEERP.Models
{
    public partial class UnitMaster
    {
       

        public int unitid { get; set; }
        public string unitname { get; set; }
      
    }
    public partial class poolcolormaster
    {


        public string colorid { get; set; }
        public string colorname { get; set; }

    }
    public partial class SaleStatusMaster
    {


        public int saleid { get; set; }
        public string salename { get; set; }

    }

}
