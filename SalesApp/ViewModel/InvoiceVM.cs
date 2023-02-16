using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class InvoiceVM
    {
        public string PdfUrl1 { get; set; }
        public string PdfUrl2 { get; set; }

        public bool ShowPDFs { get; set; }
        public bool Open2Pdf { get; set; }
        public List<string> billurl { get; set; } = new List<string>();
    }
   
}
