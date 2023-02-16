using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.ViewModel;
using SalesApp.Repository.Interface;
using SalesApp.Models;
using SalesApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SALEERP.Data;
using SALEERP.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace SalesApp.Repository
{
    public class UserRepository:IUserRepository
    {
        private Sales_ERPContext _DBERP;
        IWebHostEnvironment _hostingenv;

        public UserRepository(Sales_ERPContext dbcontext, IWebHostEnvironment _env)
        {

            this._DBERP = dbcontext;
            this._hostingenv = _env;

        }

        public List<UserLoginVM> getAllUsers()
        {

            List<UserLoginVM> _alluserdetails = new List<UserLoginVM>();
            List<LoginRoles> userroles = this._DBERP.LoginRoles.Where(a => a.Id >= 1).ToList();
            List<Roleclaim> rolclaims = this._DBERP.Roleclaim.Where(a => a.RoleId >= 1).ToList();

            // var u = rolclaims.FirstOrDefault(r => r.UserId == 5).Role.RoleName;
            IEnumerable<UserLoginVM> alluser = this._DBERP.UserLogin.ToList().Where(i => i.IsActive == true).Select(u => new UserLoginVM { CreatedDatetime = u.CreatedDatetime, UserId = u.Id, UserName = u.Name, UserEmail = u.Email, UserPass = u.Password, loginroles = BindingListUtillity.GenerateSelectList(userroles), RoleName = (string)rolclaims.Where(r => r.UserId == u.Id && r.IsActive == true).FirstOrDefault()?.Role.Name });
            //IEnumerable<UserLoginVM> alluser = this._DBERP.UserLogin.ToList().Where(i => i.IsActive == true).Select(u => new UserLoginVM { CreatedDatetime = u.CreatedDatetime, UserId = u.Id, UserName = u.Name, UserEmail = u.Email, UserPass = u.Password });

            return alluser.ToList().OrderByDescending(a => a.CreatedDatetime).ToList();

        }
        public bool AddUser(UserLoginVM _user, int id)
        {
            bool result = false, innerresult = false;
           // string uniqueFileName = UploadedFile(_user);
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                this._DBERP.UserLogin.Add(new UserLogin()
                {
                    Name = _user.UserName,
                 //   LoginPass = _user.LoginPass,
                    Password = _user.UserPass,
                    Phone = _user.UserPhone,
                    CreatedDatetime = DateTime.Now,
                    UpdatedBy = id,
                    IsActive = true,
                   // ProfilePicture = uniqueFileName
                }); ;

                result = this._DBERP.SaveChanges() > 0;
                if (result)
                {
                    int userid = this._DBERP.UserLogin.Max(p => p.Id);
                    this._DBERP.Roleclaim.Add(new Roleclaim() { RoleId = _user.RoleId, UserId = userid, CreatedDatetime = DateTime.Now, UpdatedBy = id, IsActive = true });
                    innerresult = this._DBERP.SaveChanges() > 0;
                    if (innerresult)
                    {
                        dbusertrans.Commit();
                    }
                    else
                    { dbusertrans.Rollback(); result = false; }

                }
                else { dbusertrans.Rollback(); }

            }
            return result;

        }

        public bool UpdateUser(UserLoginVM _user, int uid)
        {
            bool result = false;
            int innerresult = 0, innerresultrole = 0;
            string uniqueFileName = string.Empty;
            //if (!string.IsNullOrEmpty(_user.ProfilePic) && _user.ProfileImage == null)
            //{
            //    uniqueFileName = _user.ProfilePic;

            //}
            //else
            //{ uniqueFileName = UploadedFile(_user); }

            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {

                var entity = _DBERP.UserLogin.FirstOrDefault(item => item.Id == _user.UserId);

                if (entity != null)
                {

                    entity.Name = _user.UserName;
                    //entity.LoginPass = _user.LoginPass;
                    entity.Password = _user.UserPass;
                    entity.Phone = _user.UserPhone;
                    entity.UpdatedDatetime = DateTime.Now;
                    entity.UpdatedBy = uid;
                    entity.IsActive = true;
                   // entity.ProfilePicture = uniqueFileName;
                    this._DBERP.UserLogin.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                    
                }
                if (innerresult > 0)
                {
                    var entityrole = _DBERP.Roleclaim.FirstOrDefault(item => item.UserId == _user.UserId);

                    if (entityrole != null)
                    {

                        entityrole.UserId = _user.UserId;
                        entityrole.RoleId = _user.RoleId;

                        entityrole.UpdatedDatetime = DateTime.Now;
                        entityrole.UpdatedBy = uid;
                        entityrole.IsActive = true;
                        this._DBERP.Roleclaim.Update(entityrole);
                        innerresultrole = this._DBERP.SaveChanges();
                    }
                    else 
                    {
                        this._DBERP.Roleclaim.Add(new Roleclaim()
                        {
                            UserId = _user.UserId,
                            RoleId = _user.RoleId,
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,

                        });
                        innerresult = this._DBERP.SaveChanges();

                    }

                    dbusertrans.Commit();
                    result = true;

                }
                else { dbusertrans.Rollback(); }

            }
            return result;

        }

        public UserLoginVM EditUser(int uid)
        {
            UserLogin user = this._DBERP.UserLogin.Find(uid);
            UserLoginVM _userdetails = new UserLoginVM();
            List<LoginRoles> userroles = this._DBERP.LoginRoles.Where(a => a.Id >= 1).ToList();
            _userdetails.loginroles = BindingListUtillity.GenerateSelectList(userroles);
            _userdetails.UserName = user.Name;
            _userdetails.UserPass = user.Password;
           // _userdetails.LoginPass = user.LoginPass == null ? "" : user.LoginPass;
            _userdetails.UserPhone = user.Phone;
            _userdetails.UserId = user.Id;
            _userdetails.RoleId = this._DBERP.Roleclaim.Where(r => r.UserId == uid).Select(b=>b.RoleId).FirstOrDefault()??0;
           // _userdetails.ProfilePic = user.ProfilePicture;

            return _userdetails;
            //.Where(u => u.UserId == uid).Select(u => new UserLoginVM { UserName = u.UserName, UserEmail = u.UserEmail, UserPass = u.UserPass, roleid = u.Roleclaim.FirstOrDefault().RoleId })


        }

        public bool DeleteUser(int uid)
        {
            bool result = false;
            int innerresult = 0, innerresultrole = 0;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {

                var entity = _DBERP.UserLogin.FirstOrDefault(item => item.Id == uid);

                if (entity != null)
                {


                    entity.UpdatedDatetime = DateTime.Now;
                    entity.UpdatedBy = uid;
                    entity.IsActive = false;
                    this._DBERP.UserLogin.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }
                if (innerresult > 0)
                {
                    var entityrole = _DBERP.Roleclaim.FirstOrDefault(item => item.UserId == uid);

                    if (entityrole != null)
                    {


                        entityrole.UpdatedDatetime = DateTime.Now;
                        entityrole.UpdatedBy = uid;
                        entityrole.IsActive = false;
                        this._DBERP.Roleclaim.Update(entityrole);
                        innerresultrole = this._DBERP.SaveChanges();
                    }

                    if (innerresultrole > 0 || innerresult > 0)
                    {
                        dbusertrans.Commit();
                        result = true;
                    }
                    else
                    { dbusertrans.Rollback(); }

                }
                else { dbusertrans.Rollback(); }

            }
            return result;

        }
    }
}
