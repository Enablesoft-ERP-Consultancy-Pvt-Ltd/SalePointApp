using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CustomerDetails
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long BillId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string shippingaddress { get; set; }
        public string GSTIN { get; set; }
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
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? Isactive { get; set; }

        public virtual OrderMaster Order { get; set; }
    }
}
