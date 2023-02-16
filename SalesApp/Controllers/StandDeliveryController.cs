using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Controllers
{
    public class StandDeliveryController : Controller
    {
        IStandDeliveryRepository _stsale;
        //ICashSaleRepository _cashsale;
        public StandDeliveryController(IStandDeliveryRepository _standsale)
        {
            this._stsale = _standsale;
           // this._cashsale = _cash;

        }
        public async Task<IActionResult> Index()
        {
            CustomSaleVM cashdetails = new CustomSaleVM();
           // ViewBag.profilepic = _comm.GetLoggedInUserPic();
            cashdetails = await _stsale.Init();
            string q = HttpContext.Request.Query["pgno"].ToString();
            ViewBag.pgno = q;

            return View("Index", cashdetails);
        }
        public async Task<List<StockDetailVM>> GetStandDetails(string stockid)
        {

            List<StockDetailVM> _stock = new List<StockDetailVM>();
            try
            {
                _stock = await _stsale.GetStand(stockid);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Stocknotfound");
            }


            return _stock;

        }
        public async Task<IActionResult> AddStandInfo(CustomSaleVM standdetails)
        {
            Int64 orderid = 0;
            try
            {


                if (standdetails.cinfo != null)
                {
                    orderid = await _stsale.AddStandSale(standdetails, 1);
                    if (orderid>0)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                    }

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _stsale.GetStandSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;

            return View("Index", cashdetails);

        }
    }
}
