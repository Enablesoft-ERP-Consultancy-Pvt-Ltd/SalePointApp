using SalesApp.Repository.Interface;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SalesApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly object Session;
        private Sales_ERPContext _DBERP;
        public AccountRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public bool ManageUser(UserLoginVM _user, out int uid,out string uname,out string profilepicture,out int roletype)
        {
            bool isuserauthenticated = false;
            int userid = 0;
            uid = 0;
            uname = string.Empty;
            profilepicture = string.Empty;
            roletype = 0;
            if (!string.IsNullOrWhiteSpace(_user.UserName))
            {
                var result = this._DBERP.UserLogin.FirstOrDefault(us => us.Name == _user.UserName && us.Password == _user.UserPass && us.IsActive == true);
                //var result = this._DBERP.UserLogin.FirstOrDefault(us => us.Password == _user.UserName && us.IsActive == true);

                if (result != null)
                {
                    isuserauthenticated = true;
                    uid = result.Id;
                    uname = result.Name;
                    profilepicture = result.ProfilePicture;
                    roletype = this._DBERP.Roleclaim.Where(a => a.UserId == result.Id).Select(b => b.RoleId).FirstOrDefault() ?? 0;

                }



            }
            return isuserauthenticated;
        }
    }
}
