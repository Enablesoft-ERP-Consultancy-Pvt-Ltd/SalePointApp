using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SALEERP.Models;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Controllers
{
    public class CashSaleController : Controller
    {
        ICashSaleRepository _csale;
        ICommonRepository _comm;
        public CashSaleController(ICashSaleRepository _cashsale, ICommonRepository commonRepository)
        {
            this._csale = _cashsale;
            this._comm = commonRepository;
        }
        public async  Task<IActionResult> Index(int id)
        {
            int UID = 0;
            CashSaleVM cashdetails = new CashSaleVM();
            ViewBag.profilepic = _comm.GetLoggedInUserPic();

            try
            {
                UID = _comm.GetLoggedInUserId();
                if (UID > 0)
                {
                    cashdetails = await _csale.Init(id);
                }
                else
                {
                    return RedirectToAction("Index", "Account", new { area = "" });

                }

            }
            catch (Exception)
            {

                throw;
            }
           
            return View("Index",cashdetails);
        }
        public async Task<List<StockDetailVM>> GetStockDetails(string stockid)
        {
           // int UID = 0;
            List<StockDetailVM> _stock = new List<StockDetailVM>();
            try
            {
               // UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    _stock = await _csale.GetStock(stockid);
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Account", new { area = "" });

                //}


            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Stocknotfound");
            }
          
          
            return _stock;

        }
        public async Task<IActionResult> AddSale(CashSaleVM saleetails)
        {
            CashSaleVM cashdetails = new CashSaleVM();
            Int64 _orderid = 0;
            ModelState.Clear();
            int UID;
            try
            {
               
                UID = _comm.GetLoggedInUserId();
                if (UID > 0)
                {
                    if (string.IsNullOrEmpty(saleetails.stockno))
                    {
                        TempData["CashMessage"] = new MessageVM() { title = "Please enter", msg = "stock no." };
                        cashdetails = await _csale.GetSales(saleetails.orderid, saleetails);

                    }
                    else
                    {
                        _orderid = await _csale.AddCashSale(saleetails, UID);
                        if (_orderid == -1)
                        {

                            TempData["CashMessage"] = new MessageVM() { title = "Please enter", msg = "Stock No. Already Added!!!" };
                            cashdetails = await _csale.GetSales(saleetails.orderid, saleetails);

                        }
                        else
                        {
                            cashdetails = await _csale.GetSales(_orderid, saleetails);
                        }
                    }
                }
                else
                {
       //             await HttpContext.SignOutAsync(
       //CookieAuthenticationDefaults.AuthenticationScheme);
                  //  return View("~/Account/Index");

                  return   RedirectToAction("Index", "Account", new { area = "" });

                }

            }
            catch (Exception ex)
            {
              
                cashdetails = await _csale.Init(saleetails.mirrorid);
                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            return View("Index", cashdetails);


        }
        public async Task<IActionResult> DeleteSale(int orderItemId)
        {
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                if (UID > 0)
                {
                    await _csale.DeleteCashSale(orderItemId, UID);
                }
                else
                {
                    RedirectToAction("Logout", "Account");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            CashSaleVM cashdetails = new CashSaleVM();
            long orderid = await _csale.GetOderIdByOrderItemId(orderItemId);
            cashdetails = await _csale.GetSales(orderid,null);
            return View("Index", cashdetails);

        }
        public async Task<IActionResult> FinishSale(int orderId)
        {
            bool result;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                if (UID > 0)
                {
                    result = await _csale.AddOrderPayment(orderId, UID);
                    if (result)
                    {
                        return RedirectToAction("Index", "Mirror");

                    }
                    else
                    {
                        CashSaleVM cashdetails = new CashSaleVM();
                        cashdetails = await _csale.GetSales(orderId, null);
                        TempData["CashMessage"] = new MessageVM() { title = "Error!", msg = "Error." };
                        return View("Index", cashdetails);

                    }
                }
                else
                {
                    RedirectToAction("Logout", "Account");

                }
            }
            catch (Exception ex)
            {
                TempData["CashMessage"] = new MessageVM() { title = "Error!", msg = "Error" };
                
            }
            //CashSaleVM cashdetails = new CashSaleVM();
            //cashdetails = await _csale.GetSales(orderId,null);
            return RedirectToAction("Index", "Mirror");

        }

        public async Task<IActionResult> EditSale(Int64 orderid)
        {
            CashSaleVM cashdetails = new CashSaleVM();
            // Int64 _orderid = 0;
            int UID = 0;
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {
                    UID = _comm.GetLoggedInUserId();
                    if (UID > 0)
                    {
                        //  _orderid = await _csale.AddCashSale(saleetails, _comm.GetLoggedInUserId());
                        if (orderid > 0)
                        {

                            //TempData["NormalMessage"] = new MessageVM() { title = "", msg = "This stock no. already added!!!" };
                            cashdetails = await _csale.GetSales(orderid, null);

                        }

                        else
                        {
                            return RedirectToAction("Index", "Edit");
                        }
                    }
                    else
                    {
                        RedirectToAction("Logout", "Account");

                    }

                    // cashdetails = await _csale.GetSales(_orderid, saleetails);

                    // ViewBag.totalbalance = cashdetails.balinr;
                    ViewBag.totalbalance = 0;
                }
                else
                {
                    ModelState.AddModelError("Error", "Error:Please specify item or required fields.");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            ViewBag.pgno = 1;
         //   ViewBag.totalbalance = cashdetails.balinr;
            ViewBag.totalbalance = 0;
            return View("Index", cashdetails);


        }

        /* public async Task<IActionResult> AddSale(OrderPaymentVM paymentDetails)
         {
             OrderPaymentVM orderPayment = new OrderPaymentVM();
             Int64 _orderPaymentid = 0;
             ModelState.Clear();
             try
             {
                 //_orderPaymentid = await _csale.AddCashSale(paymentDetails, _comm.GetLoggedInUserId());
                 //cashdetails = await _csale.GetSales(_orderid);

             }
             catch (Exception ex)
             {

                 ModelState.AddModelError("Error", "Error:Add Sale Item");
             }
             //return View("Index", cashdetails);


         } */
    }
}
