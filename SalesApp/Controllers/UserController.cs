using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesApp.ViewModel;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SALEERP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace SalesApp.Controllers
{
    public class UserController : Controller
    {
        IUserRepository _user;
        ICommonRepository _comm;
        private Sales_ERPContext _DBERP;
        public UserController(IUserRepository _urepo, Sales_ERPContext dbcontext, ICommonRepository commonRepository)
        {
            this._user = _urepo;
            this._DBERP = dbcontext;
            this._comm = commonRepository;
        }
        public IActionResult Index()
        {
            List<UserLoginVM> _alluser = new List<UserLoginVM>();

            _alluser = _user.getAllUsers();
            return View(_alluser);
        }
        public ActionResult AddUser([Bind("UserName,UserPass,LoginPass,RoleId,ProfileImage")] UserLoginVM _user, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                bool result = this._user.AddUser(_user, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            return RedirectToAction("Index");

        }
        public ActionResult Update([Bind("UserId,UserName,UserPass,LoginPass,RoleId,ProfileImage,ProfilePic")] UserLoginVM _user)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._user.UpdateUser(_user, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Userid not found!");
                }


            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public PartialViewResult ShowCreatePartailView()
        {
            UserLoginVM _user = new UserLoginVM();
            _user.loginroles = this._comm.getroles().Result;

            return PartialView("ManageUser", _user);
           // return PartialView("ManageUser");
        }
        [HttpPost]
        public ActionResult ShowEditPartailView(int id)
        {
            UserLoginVM result = new UserLoginVM();
            if (ModelState.IsValid)
            {
                result = this._user.EditUser(id);

            }
            //return View(staff);
            return PartialView("UpdateUser", result);
        }
        public ActionResult GetUserDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._user.DeleteUser(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Userid not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _user.getAllUsers());
        }

    }
}
