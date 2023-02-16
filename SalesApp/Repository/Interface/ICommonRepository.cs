using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface ICommonRepository
    {
        void SetLoggedInUserId(int UserId);
        void SetUserRole(int roleid);
        void SetLoggedInUserName(string username);
        void SetLoggedInUserPic(string profilepic);
        void SetLoggedIP(string IPAddress);
        void SetUnitId(int UnitId);
        void SetBillId(int BillId);
        int GetUnitId();
        int GetBillId();
        int GetLoggedInUserId();
        string GetLoggedInUserName();
        int GetUserRoleType();
        string GetLoggedInUserPic();
        string GetLoggedIP();
        Task<List<SelectListItem>> GetCurrency();
        Task<List<SelectListItem>> GetShapes();
        Task<List<SelectListItem>> GetMarbleColor();
        Task<List<SelectListItem>> GetCategory();
        Task<List<SelectListItem>> GetSpecialAddition();
        Task<List<SelectListItem>>  getroles();
    }
}
