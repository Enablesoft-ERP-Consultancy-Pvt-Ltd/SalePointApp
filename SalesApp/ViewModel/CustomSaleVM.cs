using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Utility;
using SALEERP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.ViewModel
{
    public class CustomSaleVM
    {

        public long mirrorid { get; set; }
        public long orderid { get; set; }
        public long standorderid { get; set; }
        public DateTime mirrordate { get; set; }
        public int specialaddition { get; set; }
        public decimal? balinr { get; set; }
        public decimal? balcurrency { get; set; }
        public string currsymbol { get; set; }
        public decimal? grandtotalinr { get; set; }
        public decimal? grandtotalcurrency { get; set; }
        public string customspecialaddition { get; set; }
        public string stockno { get; set; }
        public string item_desc { get; set; }
        [DisplayName("Conv. Rate")]
        public decimal? conversionrate { get; set; }
        public int currencyid { get; set; }
        public int countryid { get; set; }
        public int nationalityid { get; set; }
        public int cardid { get; set; }
        public int cardiddebit { get; set; }
        public int paylaterid { get; set; }
        public int quantity { get; set; }
        public int title_type { get; set; }
        [DisplayName("Sale Value (Fx)")]
        public decimal totalvalue { get; set; }
        public decimal totalvalueinr { get; set; }
        public decimal? grandtotal { get; set; }
        public int userid { get; set; }
        public string shapeid { get; set; }
        public string categoryid { get; set; }
        public string standcode { get; set; }
        public string marblecolor { get; set; }
        public string color { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal PayLaterAmount { get; set; }
        public decimal PaytmAmount { get; set; }


        public int itemcount { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string size { get; set; }
        //  public string username { get; set; }

        public List<customsaledetails> cashsaledetails { get; set; }
        public List<standdetails> standsaledetails { get; set; }
        public List<paydetails> paymentdetails { get; set; }
        public customerinfo cinfo { get; set; }
        public deliveryinfo dinfo { get; set; }
        public List<SelectListItem> nationalitydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> cardtypedetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> paylaterdetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> currencydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> shapesdetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> categorydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> marblecolordetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> standcolordetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> sizeinwidth { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> sizeinheight { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> standcategory { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> countrydetails { get; set; } = new List<SelectListItem>();
        //public List<SelectListItem> specialadditions { get; set; } = new List<SelectListItem>();
        public TitleType _titletype { get; set; }

        public string titletypevalue
        {
            get { return _titletype.ToString(); }
            set { _titletype = ((TitleType)Enum.Parse(typeof(TitleType), value.ToUpper().ToString())); }
        }
        public SaleType _saletype { get; set; }

        public string saletypevalue
        {
            get { return _saletype.ToString(); }
            set { _saletype = ((SaleType)Enum.Parse(typeof(SaleType), value.ToUpper().ToString())); }
        }

        public DeliveryType _deliverytype { get; set; }

        public string deliverytypevalue
        {
            get { return _deliverytype.ToString(); }
            set { _deliverytype = ((DeliveryType)Enum.Parse(typeof(DeliveryType), value.ToUpper().ToString())); }
        }
        public PortType _porttype { get; set; }

        public string porttypevalue
        {
            get { return _porttype.ToString(); }
            set { _porttype = ((PortType)Enum.Parse(typeof(PortType), value.ToUpper().ToString())); }
        }
        public paymethod _paymethod { get; set; }

        public string paymethodvalue
        {
            get { return _paymethod.ToString(); }
            set { _paymethod = ((paymethod)Enum.Parse(typeof(paymethod), value.ToUpper().ToString())); }
        }

    }
    public class customsaledetails
    {
        public long itemorderid { get; set; }
        public string ordertype { get; set; }
        public string stockid { get; set; }
        public string itemdesc { get; set; }

        public decimal? salevalue { get; set; }

        public decimal? salevalueinr { get; set; }
        public string shape { get; set; }
        public string category { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public decimal? conversionrate { get; set; }
        public long mirrorid { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? mirrordate { get; set; }
        public string symbol { get; set; }
        public int currencyid { get; set; }
        public string currency { get; set; }


    }
    public class standdetails
    {
        public long itemorderid { get; set; }
        public string ordertype { get; set; }
        public string standdesc { get; set; }
        public string standcode { get; set; }

        public string color { get; set; }

        public string width { get; set; }

        public string height { get; set; }

    }
    public class paydetails
    {
        public long payid { get; set; }
        public string paymode { get; set; }
        public string paytype { get; set; }
        public string currency { get; set; }
        public decimal? payamount { get; set; }
        public decimal? payamountinr { get; set; }
        public string symbol { get; set; }
        public int currencyid { get; set; }

    }
    public class customerinfo
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? countryid { get; set; }
        public string nationality { get; set; }
        public string Zipcode { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string TeleCountryCode { get; set; }
        public string MobCountryCode { get; set; }
        public string Email { get; set; }
        public string PassportNo { get; set; }
        public string Airport { get; set; }

    }
    public class deliveryinfo
    {
        public string Passport { get; set; }
        public string PortName { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DeliveryFrom { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DeliveryTo { get; set; }
        public int? DelieveryType { get; set; }

        public int? PortType { get; set; }
    }
    public class calucation
    {
        public long? mirrorid { get; set; }
        public long orderid { get; set; }
        public long itemorderid { get; set; }
        public decimal? conversionrate { get; set; }

        public decimal Amount { get; set; }
        public decimal AmountINR { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal TotalAmountINR { get; set; }

    }
}
