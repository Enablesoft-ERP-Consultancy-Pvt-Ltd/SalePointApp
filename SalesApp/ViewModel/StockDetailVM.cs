using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class StockDetailVM
    {
        public string category { get; set; }
        public string itemname { get; set; }
        public string shape { get; set; }
        public string stockid { get; set; }
        public string hsncode { get; set; }
        public int finishedid { get; set; }
        public int? qualityid { get; set; }
        public string size { get; set; }
        public string marblecolor { get; set; }
        public string marblestone { get; set; }
        public decimal? price { get; set; }
    }
}
    