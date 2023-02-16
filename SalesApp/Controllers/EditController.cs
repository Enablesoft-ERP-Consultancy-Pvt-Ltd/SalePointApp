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
namespace SalesApp.Controllers
{
    public class EditController : Controller
    {
        IEditRepository _edt;
        private Sales_ERPContext _DBERP;
       // private readonly INRepository _csale;
        private readonly ICommonRepository _comm;
        public EditController(IEditRepository _edit, Sales_ERPContext dbcontext, ICommonRepository commonRepository)
        {
            this._edt = _edit;
            this._DBERP = dbcontext;
            this._comm = commonRepository;

        }
        public IActionResult Index()
        {
            ViewBag.profilepic = _comm.GetLoggedInUserPic();
            return View(_edt.getAllOrders());

        }
        public IActionResult Temp()
        {
            ViewBag.profilepic = _comm.GetLoggedInUserPic();
            return View(_edt.getAllTempOrders());

        }
        public async Task<IActionResult> DeleteSale(long itemorderid)
        {
            Int64? orderid = 0;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                orderid = await _edt.Delete(itemorderid, UID);

            }
            catch (Exception)
            {

                throw;
            }
            return View("Temp", _edt.getAllTempOrders());

        }
    }
}
