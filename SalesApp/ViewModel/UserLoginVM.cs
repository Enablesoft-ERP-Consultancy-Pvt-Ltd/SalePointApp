using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Utility;

namespace SalesApp.ViewModel
{
    public class UserLoginVM
    {
        public int UserId { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string UserName { get; set; }
     //   [Required]
        public string UserPass { get; set; }
        public string UserPhone { get; set; }
        public string UserProfilePic { get; set; }
        public string UserEmail { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<RoleclaimVM> Roleclaim { get; set; }

        public List<SelectListItem> loginroles { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string searchstring { get; set; }
      //  public PaginatedList<UserLoginVM> Users { get; set; }

        //public string Rolename { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public int CurrentPage { get; set; } = 1;
        //public int Count { get; set; }
        //public int PageSize { get; set; } = 10;

        //public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        //public List<UserLoginVM> Data { get; set; }

        //public async Task OnGetAsync()
        //{
        //    Data = await GetPaginatedResult(CurrentPage, PageSize);
        //    Count = await GetCount();
        //}
    }
}
