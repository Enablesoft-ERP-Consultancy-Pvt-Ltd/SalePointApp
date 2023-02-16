using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.ViewModel;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Http;

namespace SalesApp.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private Sales_ERPContext _DBERP;
        public static int loggedInUserId;
        public static int loggedinuserrole;
        public static int loggedUnitId;
        public static int loggedBillNoUnitWise;
        public static string loggedInUserName;
        public static string loggedInUserPicture;
        public static string loggedIP;
        public CommonRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;
        }

        public CommonRepository()
        { }
        public async Task<List<SelectListItem>> GetWidth()
        {
            List<SelectListItem> _getwidth = new List<SelectListItem>();
           // List<SelectListItem> _getlength = new List<SelectListItem>();
            List<Size> _sizemaster = await this._DBERP.Size.ToListAsync();
            _getwidth = BindingListUtillity.GenerateSelectListWidth(_sizemaster);
          //  _getlength = BindingListUtillity.GenerateSelectListLength(_sizemaster);
            return _getwidth;
        }
        public async Task<List<SelectListItem>> GetLenght()
        {
            List<SelectListItem> _getlength = new List<SelectListItem>();
            //  List<Size> _sizemaster = await this._DBERP.Size.Where(a => a.LengthInch >= 15 && a.LengthInch<30 && a.Shapeid==4 && a.SizeId<890 ).OrderBy(O=>O.LengthInch).ToListAsync();
            List<Size> _sizemaster = await this._DBERP.Size.Where(a => a.Shapeid == 4 && a.SizeId < 890).OrderBy(O => O.LengthInch).ToListAsync();
            _getlength = BindingListUtillity.GenerateSelectListLength(_sizemaster);
            return _getlength;
        }
        public async Task<List<SelectListItem>> GetItemName()
        {
            List<SelectListItem> _getItem = new List<SelectListItem>();
            List<ItemMaster> _Itemmaster = await this._DBERP.ItemMaster.Where(i => i.CategoryId==55).ToListAsync();
            _getItem = BindingListUtillity.GenerateSelectList(_Itemmaster);
            return _getItem;
        }
        public async Task<List<SelectListItem>> GetCurrency()
        {
            List<SelectListItem> _getcurrency = new List<SelectListItem>();
            List<CurrencyMaster> _currencymaster = await this._DBERP.CurrencyMaster.Where(i => i.IsActive == true).OrderBy(p => p.Priority >0 ? 0 : 1).ThenBy(p => p.Priority).ToListAsync();
            _getcurrency = BindingListUtillity.GenerateSelectList(_currencymaster);
            return _getcurrency;
        }
        public async Task<List<SelectListItem>> GetSpecialAddition()
        {
            List<SelectListItem> _getspecialaddition = new List<SelectListItem>();
            List<SpecialEdition> _samaster = await this._DBERP.SpecialEdition.Where(i => i.IsActive == true).ToListAsync();
            _getspecialaddition = BindingListUtillity.GenerateSelectList(_samaster);
            return _getspecialaddition;
        }
        public async Task<List<SelectListItem>>  getroles()
        {
            List<SelectListItem> _getroles = new List<SelectListItem>();
            List<LoginRoles> userroles = await this._DBERP.LoginRoles.Where(a => a.IsActive == true ).ToListAsync();
            _getroles = BindingListUtillity.GenerateSelectList(userroles);
            return _getroles;
        }

        public async Task<List<SelectListItem>> GetShapes()
        {
            List<SelectListItem> _getshapes = new List<SelectListItem>();
          //  List<Shape> allshapes = await this._DBERP.Shape.Where(a=>a.Type !=null).OrderBy(T=>T.ShapeName).ToListAsync();
            List<Shape> allshapes = await this._DBERP.Shape.Where(a => a.Type != null).OrderBy(p => p.Type > 0 ? 0 : 1).ThenBy(p => p.Type).ThenBy(a => a.ShapeName).ToListAsync();
            _getshapes = BindingListUtillity.GenerateSelectList(allshapes);
            return _getshapes;
           // throw new NotImplementedException();
        }

        public async Task<List<SelectListItem>> GetMarbleColor()
        {
            var ids = new List<int?> { 73,43,6,44,74 };
            List<SelectListItem> _getmarble = new List<SelectListItem>();
            List<DesignIntricacyComponentPlacementMarblecolor> allmarblecolor = await this._DBERP.DesignIntricacyComponentPlacementMarblecolor.Where(a=> ids.Contains(a.Id)).OrderBy(a=> a.Id).ToListAsync();
            _getmarble = BindingListUtillity.GenerateSelectList(allmarblecolor);
            return _getmarble;
        }
        public async Task<List<SelectListItem>> GetStandColor()
        {
           // var ids = new List<int?> { 73, 44, 74, 43, 6 };
            List<SelectListItem> _getmarble = new List<SelectListItem>();
            List<DesignIntricacyComponentPlacementMarblecolor> allmarblecolor = await this._DBERP.DesignIntricacyComponentPlacementMarblecolor.Where(a => a.TypeId == 13 && a.OrderType !=null).OrderBy(a => a.OrderType).ToListAsync();
            _getmarble = BindingListUtillity.GenerateSelectListcolor(allmarblecolor);
            return _getmarble;
        }

        public async Task<List<SelectListItem>> GetCategory()
        {
            List<SelectListItem> _getcategory = new List<SelectListItem>();
            List<ItemCategoryMaster> allcategory = await(from m in this._DBERP.ItemCategoryMaster 
                                                                                      join od in this._DBERP.CategorySeparate
                                                                                     on m.CategoryId equals od.Categoryid select new ItemCategoryMaster
                                                                                     {
                                                                                        CategoryId=m.CategoryId,
                                                                                         CategoryName=m.CategoryName,
                                                                                         priortiy=m.priortiy

                                                                                     }

                                                                                   
                                                                                    ).Distinct().OrderBy(p => p.priortiy > 0 ? 0 : 1).ThenBy(p => p.priortiy).ThenBy(a => a.CategoryName).ToListAsync();



            _getcategory = BindingListUtillity.GenerateSelectList(allcategory);
            return _getcategory;
        }
        public async Task<List<SelectListItem>> GetNationality()
        {
            List<SelectListItem> _getnationality = new List<SelectListItem>();
         //   List<Nationality> _nationalitymaster = await this._DBERP.Nationality.ToListAsync();
            List<CountriesMaster> _nationalitymaster = await this._DBERP.CountriesMaster.Where(i => i.IsActive == true).OrderBy(p => p.Priority > 0 ? 0 : 1).ThenBy(p => p.Priority).ThenBy(a => a.Name).ToListAsync();
            _getnationality = BindingListUtillity.GenerateSelectList(_nationalitymaster);
            return _getnationality;
        }
        public async Task<List<SelectListItem>> GetCardType()
        {
            List<SelectListItem> _getcardtype = new List<SelectListItem>();
            List<CardTypeMaster> _cardtypemaster = await this._DBERP.CardTypeMaster.Where(i => i.IsActive == true && i.CardCode == "C").ToListAsync();
            _getcardtype = BindingListUtillity.GenerateSelectList(_cardtypemaster);
            return _getcardtype;
        }
        public async Task<List<SelectListItem>> GetPayLaterType()
        {
            List<SelectListItem> _getpaylater = new List<SelectListItem>();
            List<CardTypeMaster> _paylatermaster = await this._DBERP.CardTypeMaster.Where(i=>i.IsActive==true  && i.CardCode=="P").ToListAsync();
            _getpaylater = BindingListUtillity.GenerateSelectList(_paylatermaster);
            return _getpaylater;
        }
      
        public async Task<List<SelectListItem>> GetCountries()
        {
            List<SelectListItem> _getcountry = new List<SelectListItem>();
            //  List<CountriesMaster> _countrymaster = await this._DBERP.CountriesMaster.Where(c=>c.IsActive==true).ToListAsync();
            List<CountriesMaster> _countrymaster = await this._DBERP.CountriesMaster.Where(i => i.IsActive == true).OrderBy(p => p.Priority > 0 ? 0 : 1).ThenBy(p => p.Priority).ThenBy(a=>a.Name).ToListAsync();
            _getcountry = BindingListUtillity.GenerateSelectList(_countrymaster);
            return _getcountry;
        }

        public void SetLoggedInUserId(int UserId)
        {
            loggedInUserId = UserId;
        }
        public int GetLoggedInUserId()
        {
            return loggedInUserId;
        }
       
        //public int GetUnitId()
        //{
        //    return loggedInUserId;
        //}
        public void SetLoggedInUserName(string Username)
        {
            loggedInUserName = Username;
        }
        public string GetLoggedInUserName()
        {
            return loggedInUserName;
        }
        public void SetLoggedInUserPic(string profilepic)
        {
            loggedInUserPicture = profilepic;
        }
       
        public string GetLoggedInUserPic()
        {
            return loggedInUserPicture;
        }
        public void SetUnitId(int UnitId)
        {
            loggedUnitId = UnitId;
        }

        public int GetUnitId()
        {
            return loggedUnitId;
        }

        public void SetBillId(int BillId)
        {
            loggedBillNoUnitWise = BillId;
        }

        public int GetBillId()
        {
            return loggedBillNoUnitWise;
        }

        public void SetLoggedIP(string IPAddress)
        {
            loggedIP = IPAddress;
        }

        public string GetLoggedIP()
        {
            return loggedIP;
        }

        public void SetUserRole(int roleid)
        {
            loggedinuserrole = roleid;
        }

        public int GetUserRoleType()
        {
            return loggedinuserrole;
        }
    }
}
