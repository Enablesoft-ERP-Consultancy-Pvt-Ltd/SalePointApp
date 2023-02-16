using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class MirrorDetailVM
    {
        public long mirrorid { get; set; } = 0;
        public int? agentid { get; set; }
        public int? demoid { get; set; }
        public DateTime? mirrordate { get; set; } = null;
        public string name { get; set; } = string.Empty;
        public string mob { get; set; } = string.Empty;
        public string demoname { get; set; } = string.Empty;
        public string agentcode { get; set; } = string.Empty;
        public string language { get; set; } = string.Empty;
        public List<MirrorDetailVM> driver { get; set; }
        public List<MirrorDetailVM> excursion { get; set; }
        public List<MirrorDetailVM> principle { get; set; }
        public List<MirrorDetailVM> guide { get; set; }
        public List<MirrorDetailVM> teamlead { get; set; }
        public List<MirrorDetailVM> teamescort { get; set; }
        public List<MirrorDetailVM> demo { get; set; }
        public List<string> demonamelist { get; set; } = new List<string>();
        public string p { get; set; } = string.Empty;
        //public string Vehicle { get; set; } = string.Empty;
        //public bool parchiamount { get; set; } 
        //public int? VehicleTypeid { get; set; } = 0;
        public List<driverdetails> d_list = new List<driverdetails>();
        public string searchstring { get; set; } = string.Empty;
       // public List<AgentContactVM> a = new List<AgentContactVM>();
        public int? Pax { get; set; }
        public int? Countryid { get; set; }
        public int? Languageid { get; set; }
        public long? PoolId { get; set; }
        public int? SeriesId { get; set; }
        public string vehicleNo { get; set; }
       
        public int? vehicletypeid { get; set; }
        public bool? Isparchi { get; set; }
        public bool? Isdemo { get; set; }
    }
    public class driverdetails
    {
        public string name { get; set; }
        public string mob { get; set; }
     

    }
    public class Totalmirrorandsaledetails
    {
        public long? orderid { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? mirrordate { get; set; }
        public long? itemid { get; set; }
        public long? mirrorid { get; set; }
        public long salevalue { get; set; }
        public int? unitid { get; set; }
        public int? cancelstatus { get; set; }

    }
    public class MirrorSaleDetails
    {

        public int? noofarival_unit1 { get; set; }

        public int? noofsales_unit1 { get; set; }

        public long? totalsalevalue_unit1 { get; set; }
        public int? noofarival_unit2 { get; set; }

        public int? noofsales_unit2 { get; set; }

        public long? totalsalevalue_unit2 { get; set; }


    }
}
