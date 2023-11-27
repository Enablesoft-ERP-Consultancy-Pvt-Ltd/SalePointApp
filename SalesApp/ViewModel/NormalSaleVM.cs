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
    public class NormalSaleVM
    {

        public long mirrorid { get; set; }
        public long? orderid { get; set; }
        public long billid { get; set; }
        public int finishedid { get; set; }
        public string hsncode { get; set; }
        public long standbillid { get; set; }
        public long standorderid { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? mirrordate { get; set; }
        public int specialaddition { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        // [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? balinr { get; set; }
        public decimal? tax { get; set; }

        public decimal? taxableamount { get; set; }
        public decimal? balcurrency { get; set; }
        //  [DisplayFormat(DataFormatString = "{0:G29}")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? grandtotalinr { get; set; }
        // [DisplayFormat(DataFormatString = "{0:G29}")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? grandtotalinrCM { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        // [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? grandtotalinrOF { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? grandtotalCM { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        // [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? grandtotalOF { get; set; }

        public decimal? grandtotalcurrency { get; set; }
        public string customspecialaddition { get; set; }
        public string currsymbol { get; set; }

        //public string hsncode { get; set; }
        public string elephantid { get; set; }
        public string passport { get; set; }
        [DisplayName("Stock no.")]
        [Required]
        public string stockno { get; set; }
        [Required]
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
        [DisplayName("Discount")]
        public Decimal discountper { get; set; }
        public decimal discountamout { get; set; }
        public int discountpercm { get; set; }
        public int discountperof { get; set; }
        public int title_type { get; set; }
        [DisplayName("Sale Value")]
        public decimal? totalvalue { get; set; }
        public decimal totalvalueinr { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? grandtotal { get; set; }
        public int userid { get; set; }
        public string shapeid { get; set; }
        public string categoryid { get; set; }
        public string standcode { get; set; }
        public string marblecolor { get; set; }
        public string color { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Cash Amount Must Be Numeric")]
        public decimal? CashAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? PayLaterAmount { get; set; }
        public decimal? PaytmAmount { get; set; }
        public string standsaletype { get; set; }

        public int itemcount { get; set; }
        public int unitid { get; set; }
        public int? saletype { get; set; }

        public string width { get; set; }
        public string height { get; set; }
        public string size { get; set; }
        public string DelieveryTypeName { get; set; }
        public string PortTypeName { get; set; }
        //  public string username { get; set; }
        public string IPaddress { get; set; }
        public List<Ncashsaledetails> cashsaledetails { get; set; }
        public List<Ncashsaledetails> cashsaledetailsCM { get; set; }

        public List<Ncashsaledetails> cashsaledetailsOF { get; set; }
        public List<Nstanddetails> standsaledetails { get; set; }
        public List<Nstanddetails> standsaledetailsOF { get; set; }
        public List<Nstanddetails> standsaledetailsCM { get; set; }
        public List<Npaydetails> paymentdetails { get; set; }
        public List<Npaydetails> paymentdetailsOF { get; set; }
        public List<Npaydetails> paymentdetailsCM { get; set; }
        public Ncustomerinfo cinfo { get; set; }
        public Nuserinfo userinfo { get; set; }
        public List<Nuserinfo> userdetails { get; set; }
        public Ndeliveryinfo dinfo { get; set; }
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
        public List<NspecialAddition> spaddition { get; set; }
        public List<SelectListItem> specialadditions { get; set; } = new List<SelectListItem>();
        //public List<SelectListItem> specialadditions { get; set; } = new List<SelectListItem>();
        public TitleType _titletype { get; set; }
        public bool titlemr { get; set; }
        public bool issameaddress { get; set; }
        public bool isotheraddress { get; set; }
        public bool isgiftitem { get; set; }
        public bool titlemrs { get; set; }
        public decimal DiscountedAmount { get ; set; }
        public string titletypevalue
        {
            get { return _titletype.ToString(); }
            set { _titletype = ((TitleType)Enum.Parse(typeof(TitleType), value.ToUpper().ToString())); }
        }
        [Required(ErrorMessage = "select any one option")]
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
    public class Ncashsaledetails
    {
        public long itemorderid { get; set; }
        public long orderid { get; set; }
        public long billid { get; set; }
        public string ordertype { get; set; }
        public string symbol { get; set; }
        public string hsncode { get; set; }
        public string stockid { get; set; }
        public string itemdesc { get; set; }
        public string additiondesc { get; set; }
        public long InvoiceID { get; set; }
        [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? salevalue { get; set; }
        [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? salevalueinr { get; set; }
        public long mirrorid { get; set; }
        public int unitid { get; set; }
        public Decimal? discountper { get; set; }
        public int? saletype { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? orderdate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? mirrordate { get; set; }
        public decimal? conversionrate { get; set; }
        public int currencyid { get; set; }
        public string currency { get; set; }
        public string username { get; set; }
        public bool? isgiftitem { get; set; }

    }

    public class Nstanddetails
    {
        public long itemorderid { get; set; }
        public string ordertype { get; set; }
        public string standdesc { get; set; }
        public string standcode { get; set; }

        public string color { get; set; }

        public string width { get; set; }

        public string height { get; set; }
        public long standbillid { get; set; }

    }
    public class Npaydetails
    {
        public long payid { get; set; }
        public string paymode { get; set; }
        public string paytype { get; set; }
        public string currency { get; set; }
        public decimal? payamount { get; set; }
        [DisplayFormat(DataFormatString = "{0:G29}")]
        public decimal? payamountinr { get; set; }
        public string symbol { get; set; }
        public string mainsymbol { get; set; }
        public int currencyid { get; set; }
        public int? paymodeid { get; set; }

    }
    public class Ncustomerinfo
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ShippingAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? countryid { get; set; }
        public string nationality { get; set; }
        public string Zipcode { get; set; }
        public string GSTIN { get; set; }
        public string Mobile { get; set; }
        [MaxLength(10)]
        public string Telephone { get; set; }
        [MaxLength(2)]
        public string TeleCountryCode { get; set; }
        public string MobCountryCode { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string PassportNo { get; set; }
        public string Airport { get; set; }
        public int? titlevalue { get; set; }


    }
    public class Nuserinfo
    {
        public int userid { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
        public class Ndeliveryinfo
    {
        public string Passport { get; set; }
        public string PortName { get; set; }


        [DataType(DataType.Date)]
        public DateTime? DeliveryFrom { get; set; }
        [DataType(DataType.Date)]

        public DateTime? DeliveryTo { get; set; }
        public int? DelieveryType { get; set; }

        public bool isotherstate { get; set; }
        public int? PortType { get; set; }

        public string ewaybill { get; set; }
        public string modeoftransport { get; set; }
        public string transportname { get; set; }
        public string vehicleno { get; set; }

    }
    public class Ncalucation
    {
        public long? mirrorid { get; set; }
        public long orderid { get; set; }
        public long billid { get; set; }
        public long itemorderid { get; set; }
        public decimal? conversionrate { get; set; }

        public decimal Amount { get; set; }
        public decimal AmountINR { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal TotalAmountINR { get; set; }

    }
    public class NspecialAddition
    {
       
        public string desc { get; set; }
    }
    public class standsizedetails
    {

        public string text { get; set; }
        public string value { get; set; }
    }
    public class StandSearchVM
    {
        public string label { get; set; }
        public string id { get; set; }
    }
}
