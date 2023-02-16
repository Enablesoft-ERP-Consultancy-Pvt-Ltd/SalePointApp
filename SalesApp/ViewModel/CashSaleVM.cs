using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalesApp.ViewModel
{
    public class CashSaleVM
    {

        public long mirrorid { get; set; }

        public long orderid { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? mirrordate { get; set; }
        public int  specialaddition { get; set; }
        public int unitid { get; set; }
        public string customspecialaddition { get; set; }
        public string stockno { get; set; }
        public string elephantid { get; set; }
        public string currsymbol { get; set; }
        public string item_desc { get; set; }
        [DisplayName("Conv. Rate")]
        public decimal? conversionrate { get; set; }
        public int currencyid { get; set; }
        public int quantity { get; set; }
        public int sale_type { get; set; }
        [DisplayName("Sale Value")]
        public decimal? totalvalue { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? grandtotal { get; set; }
        public int userid { get; set; }
        public int itemcount { get; set; }
        public List<cashsaledetails> cashsaledetails { get; set; }
        public List<SelectListItem> currencydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> specialadditions { get; set; } = new List<SelectListItem>();
        public SaleType _saletype { get; set; }

        public string saletypevalue
        {
            get { return _saletype.ToString(); }
            set { _saletype = ((SaleType)Enum.Parse(typeof(SaleType), value.ToUpper().ToString())); }
        }


    }
    public class cashsaledetails
    {
        public long itemorderid { get; set; }
        public string ordertype { get; set; }
        public string symbol { get; set; }
        public string stockid { get; set; }
        public string itemdesc { get; set; }
        [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? salevalue { get; set; }
        [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? salevalueinr { get; set; }
        public long mirrorid { get; set; }

       
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? mirrordate { get; set; }
       
        public decimal? conversionrate { get; set; }
        public int currencyid { get; set; }
        public string currency { get; set; }
    }
}
