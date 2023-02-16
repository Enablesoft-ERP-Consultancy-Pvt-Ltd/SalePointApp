using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace SalesApp.Controllers
{
    public class AccountController : Controller
    {
       
            IAccountRepository _acc;
            ICommonRepository _comm;
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }
        public AccountController(IAccountRepository _accrepo, ICommonRepository comm, IConfiguration _config, ILogger<AccountController> logger)
            {
                this._acc = _accrepo;
                this._comm = comm;
                this.Configuration = _config;
            this._logger = logger;

            }
            public async Task<IActionResult> Index()
            {
            UserLoginVM _init= new UserLoginVM();
            
            try
                {
                    if (ModelState.IsValid)
                    {
                    _init.loginroles =await this._comm.getroles();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return View(_init);
            }
            public async Task<IActionResult> Login(UserLoginVM _usr)
            {
                int userid = 0,roletype=0;
            string uname = string.Empty;
            string profilepic = string.Empty;
            try
                {
                var addlist = Dns.GetHostEntry(Dns.GetHostName());
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                string unit1ip = Configuration.GetSection("IPSettings").GetSection("UNIT1").Value;
                string unit2ip = Configuration.GetSection("IPSettings").GetSection("UNIT2").Value;
                string unit12ip = Configuration.GetSection("IPSettings").GetSection("UNIT21").Value;
                string unit21ip = Configuration.GetSection("IPSettings").GetSection("UNIT12").Value;
                string GetHostName = addlist.AddressList.ToString();
                // string GetIPV6 = addlist.AddressList[0].ToString();
                string clientIP = ip.ToString();


                _comm.SetLoggedIP(clientIP);
                if (unit1ip == clientIP || unit12ip == clientIP)
                {
                    _comm.SetUnitId(1);

                }
                else if (unit2ip == clientIP || unit21ip == clientIP)
                {
                    _comm.SetUnitId(2);

                }
                else
                { _comm.SetUnitId(1); }

                if (ModelState.IsValid)
                    {
                        bool result = this._acc.ManageUser(_usr, out userid,out uname,out  profilepic,out roletype);
                        if (result)
                        {
                            var uClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, uname),
               
                new Claim(ClaimTypes.PrimarySid, userid.ToString()),
                 };

                        var UIdentity = new ClaimsIdentity(uClaims, "User Identity");

                            var uPrincipal = new ClaimsPrincipal(new[] { UIdentity });
                            await HttpContext.SignInAsync(uPrincipal);
                        HttpContext.Session.SetString("UID", userid.ToString());
                        HttpContext.Session.SetString("UNAME", uname);
                        HttpContext.Session.SetString("PIC", profilepic == null ? String.Empty : profilepic);
                        HttpContext.Session.SetString("roleid", roletype.ToString());
                        _comm.SetLoggedInUserId(userid);
                        _comm.SetLoggedInUserName (uname);
                        _comm.SetLoggedInUserPic(profilepic);
                        _comm.SetUserRole(roletype);
                        
                       // ViewBag.roletype = roletype;
                        // userid = result.UserId;
                        return RedirectToAction("Index", "Mirror");
                        }
                        else { ModelState.AddModelError(string.Empty, "Username did not matched.Invalid Login!"); }
                        

                    }

                }

                catch (Exception)
                {
                    throw;
                }
                return View("Index",null);
            }

            public async Task<IActionResult> Logout()
            {
           
                await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
           
                return View();
            }
        }
    
}
