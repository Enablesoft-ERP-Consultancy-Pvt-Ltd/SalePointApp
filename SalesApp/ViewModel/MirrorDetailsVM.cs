using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesApp.Models;
namespace SalesApp.ViewModel
{
    public class MirrorDetailsVM
    {
        public string userprofilepic { get; set; }
        public long MirrorId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime? MirrorDate { get; set; }
        public int? PrincipleAgentId { get; set; }
        public List<multiagent> PAgentID_List { get; set; }
        public int? ExcursionAgentId { get; set; }
        public List<multiagent> EAgentID_List { get; set; } 
        public int? DemoPersonId { get; set; }
        public List<multiagent> DemoAgentID_List { get; set; } 
        public int? TourLeaderId { get; set; }
        public List<multiagent> LeaderAgentID_List { get; set; } 
        public int? TourEscortId { get; set; }
        public List<multiagent> EscortAgentID_List { get; set; } 
        public int? TourGuideId { get; set; }
        public List<multiagent> GuideAgentID_List { get; set; } 
        public List<DriverDetails> Driver_List { get; set; }
        
        public List<MirrorDetailVM> Mirrors { get; set; } = new List<MirrorDetailVM>();
        public int? DriverId { get; set; }
        public int? Pax { get; set; }
        [DisplayName("Driver Name")]
        public string Drivername { get; set; }
        [DisplayName("Mobile No.")]
        public string Mob { get; set; }
        [DisplayName("Vehicle No.")]
        public string vehicleNo { get; set; }
       [DisplayName("Vehicle")]
        public int? vehicletypeid { get; set; }
        [DisplayName("Parchi(50 Rs.)")]
        public bool? IsParchi { get; set; }
        public int? Countryid { get; set; }
        public int? Languageid { get; set; }
        public long? PoolId { get; set; }
        public int? SeriesId { get; set; }
        public int? StatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Createdby { get; set; }
        public bool? IsActive { get; set; }
        public string Username { get; set; }
        public List<SelectListItem> vehicledetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> seriesdetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> countrydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> languagedetails { get; set; } = new List<SelectListItem>();

        public MirrorSaleDetails _sale { get; set; } = new MirrorSaleDetails();
        //public virtual ICollection<PayDetails> PayDetails { get; set; }

    }
    public class DriverDetails
    {
        [DisplayName("Driver Name")]
        public string Drivername { get; set; } = string.Empty;
        [DisplayName("Mobile No.")]
        public string Mob { get; set; } = string.Empty;
        [DisplayName("Vehicle No.")]
        public string vehicleNo { get; set; } = string.Empty;
        // [DisplayName("Driver Name")]
        public decimal parchiamount { get; set; }
        [DisplayName("Parchi")]
        public bool isparchiamount { get; set; }
       
        public int? vehicletypeid { get; set; } 

        public Int32? agentId { get; set; }


    }

    public class multiagent
    {
        public string agentname { get; set; }
        public int? agentid { get; set; }
        public string Mob { get; set; }

    }
   
}
