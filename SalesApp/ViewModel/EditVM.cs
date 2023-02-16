using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class EditVM
    {
        public List<Edititemprintdeatils> Orders { get; set; } = new List<Edititemprintdeatils>();
      
    }
    public class  Edititemprintdeatils
    {
        public long mirrorid { get; set; }
        public int? agentid { get; set; }
        public int? saletype { get; set; }
        public long? orderid { get; set; }
        public long? itemorderid { get; set; }
        public string billdesc { get; set; }
        public long? billid { get; set; }
        public int? unit { get; set; }
        public string prefix { get; set; }
        public decimal? stockvalue { get; set; }
        public string customername { get; set; } = string.Empty;
        public string stockdetails { get; set; }
        public string stockdesc { get; set; }
        public DateTime? odate { get; set; }
        public List<string> desc { get; set; }
        public List<string> bills { get; set; }

    }
   
        public class Editbilldetails
    {
        public string billno { get; set; }
    }
    public class Edititemdetails
    {
        public string itemdesc { get; set; }
    }
}
