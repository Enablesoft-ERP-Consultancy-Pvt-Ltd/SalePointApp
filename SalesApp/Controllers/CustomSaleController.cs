using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using System.Text;

namespace SalesApp.Controllers
{
    public class CustomSaleController : Controller
    {
        ICustomSaleRepository _cussale;
        IWebHostEnvironment _hostingenv;
        ICommonRepository _comm;
      //  private readonly IHostingEnvironment _honstingEnvironment;
        public CustomSaleController(ICustomSaleRepository _customsale, IWebHostEnvironment _env, ICommonRepository commonRepository)
        {
            this._cussale = _customsale;
            this._hostingenv = _env;
            this._comm = commonRepository;
          //  _honstingEnvironment = hostingEnvironment;

        }
        public async Task<IActionResult> Index(long id)
        {
            CustomSaleVM cashdetails = new CustomSaleVM();
            ViewBag.profilepic = _comm.GetLoggedInUserPic();
            cashdetails = await _cussale.Init(id);
            string q = HttpContext.Request.Query["pgno"].ToString();
            ViewBag.pgno = q;

            return View("Index", cashdetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomSale(CustomSaleVM saleetails, IFormFile file)
        {
            Int64 orderid = 0;
            try
            {
                
                long size = 0;
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var filepathtoreturn = string.Empty;
                    string folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + saleetails.orderid;
                    string filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + saleetails.orderid + $@"\{files[0].FileName}";
                    bool folderExists = Directory.Exists(folderpath);
                    if (!folderExists)
                        Directory.CreateDirectory(folderpath);
                    size += files[0].Length;

                    using (System.IO.FileStream fs = System.IO.File.Create(filename))
                    {
                        files[0].CopyTo(fs);
                        fs.Flush();
                    }
                }
                orderid=    await _cussale.AddCashSale(saleetails, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
              
                saleetails = await _cussale.Init(0);
            }
            // CustomSaleVM customsaledetails = new CustomSaleVM();
            ModelState.Clear();
            saleetails = await _cussale.GetSales(orderid, saleetails);
            ViewBag.pgno = 1;
           // ViewBag.totalbalance = saleetails.balinr;
            return View("Index", saleetails);

        }
        public async Task<IActionResult> DeleteSale(int itemorderid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _cussale.DeleteCashSale(itemorderid, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales((Int64)orderid, null);

            ViewBag.pgno = 1;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);
        }

        public async Task<IActionResult> FinishSale(int orderid)
        {
            bool result;
            try
            {
                result = await _cussale.FinishCashSale(orderid, _comm.GetLoggedInUserId());
                if (result)
                {
                    return RedirectToAction("Index", "Mirror");

                }
                else
                {
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                    // return View("Index", standdetails);

                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Finished Sale Item");
            }
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(orderid, null);
            ViewBag.pgno = 5;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddCustomerInfo(CustomSaleVM saledetails)
        {
            bool result = true;
            try
            {


                if (saledetails.cinfo != null)
                {
                result=    await _cussale.AddCustomerinfo(saledetails, _comm.GetLoggedInUserId());
                    if(result)
                    {
                        TempData["CustomerUserMessage"] = new MessageVM() { title = "Success!", msg = "Customer details has been updated succesfully!!!" };

                    }

                }
            }
            catch (Exception)
            {
                TempData["CustomerUserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                
            }
            ModelState.Clear();
              CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(saledetails.orderid, saledetails);
            ViewBag.pgno = 2;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddStandInfo(CustomSaleVM standdetails)
        {
            bool result = true;
            try
            {


                if (standdetails.orderid>=0)
                {
                  result=  await _cussale.AddStandSale(standdetails, _comm.GetLoggedInUserId());
                    if (result)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                       // return View("Index", standdetails);

                    }

                }
                else
                {
                   // ModelState.IsValid;
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                   // return View("Index", standdetails);

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddDeliveryInfo(CustomSaleVM standdetails)
        {
            bool result = true;
            try
            {


                if (standdetails.orderid >= 0)
                {
                    result = await _cussale.AddDeliveryDetails(standdetails, _comm.GetLoggedInUserId());
                    if (result)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                        // return View("Index", standdetails);

                    }

                }
                else
                {
                    // ModelState.IsValid;
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                    // return View("Index", standdetails);

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> DeleteStand(int itemorderid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _cussale.DeleteCashSale(itemorderid, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 3;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);
        }

        public async Task<IActionResult> AddOrderPayment(CustomSaleVM paydetails)
        {
            bool result = true;
            try
            {


                if (paydetails.orderid >= 0)
                {
                    result = await _cussale.AddSalePayment(paydetails, _comm.GetLoggedInUserId());
                    if (result)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Payment updated successfully." };

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                        // return View("Index", standdetails);

                    }

                }
                else
                {
                    // ModelState.IsValid;
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                    // return View("Index", standdetails);

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", paydetails);

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(paydetails.orderid, paydetails);
            ViewBag.pgno = 4;
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }
        public async Task<IActionResult> DeletePayment(int payid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _cussale.DeletePayment(payid, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 4;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);
        }
        public IActionResult ExportToPDF(string submit)
        {
            //Initialize HTML to PDF converter 
            StringBuilder htmlfile = new StringBuilder();
            htmlfile.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Emailer</title><style>table{			border: 1px solid #333;	}td{padding: 5px;}</style></head><body><table width='900' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td height='15px' style='background-color: #000!important;padding-left: 5px;text-align: center;color: #ffffff!important; '><h4 style='font-family: inherit; font-weight: 500;color: #fff !important; margin-top: 10px; margin-bottom: 10px; font-size: 18px; font-family: Gotham, Helvetica, Arial, sans-serif;'>Invoice</h4></td></tr><tr><td height='5'></td></tr><tr><td align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 22px;  font-style: normal;'>Akbar International</td></tr><tr><td height='15' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 16px;  font-style: normal;'>Manufactures and exporters of Inlaid Articles</td></tr><tr><td style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; float:left; font-style: normal;'><table width='900' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td width='290' valign='top' style='border-right:solid 1px #333'>GST no: 09AKHPS5832P1ZA<br>State Uttar: Pardesh<br>State Code: 09 </td><td width='309' valign='top' style='border-right:solid 1px #333; '><span style='font-size: 22px'> Cash Memo</span><br>		Invoice No.: <strong>Cm/1/1383</strong></td><td width='299' valign='top'><strong>Mohd Javed</strong><br>Date: 04-03-2020<br></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:left; font-style: normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody><tr><td width='85'>Name</td><td width='409'><strong>Mr abc</strong></td><td width='95'>Nationality</td><td width='311'><strong>USA</strong></td></tr><tr><td>Address</td><td><strong>HDFC Bank ltd Sanjay Place Agra (INDIA)</strong></td><td>Country</td><td><strong>USA</strong></td></tr><tr><td>State</td>   <td><strong>Uttar Pardesh</strong></td><td>City</td><td><strong>abc</strong></td></tr><tr><td>Zip code</td><td><strong>HDFC0000121</strong></td><td>Passport No.</td><td><strong>21114214</strong></td></tr><tr><td>Email</td><td>&nbsp;</td><td>Mobile No.</td><td><strong>96796796799</strong></td></tr><tr><td>Phone</td><td><strong>999999999</strong></td><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table></td></tr><tr><td style = 'font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:left; font-style: normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding = '0' style = 'border:none'><tbody><tr><td width = '450'><strong> HSN CODE: 97024555145(Original Engrawing) </strong></td><td width = '450'><strong> HSN CODE: 97024555145(Sculptures) </strong></td></tr><tr><td colspan = '2' align = 'center'  style = 'font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:centre; font-style: normal;'><strong>(Original engrawing(work of art) inlaid with semi - precious Stones handmade in Agra, India) </strong></td></tr></tbody></table></td></tr><tr><td valign = 'top' style = 'font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:left; font-style: normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding = '0' style = 'border:solid 1px #ccc'><tbody><tr><td colspan = '4' valign = 'top'  height = '10px'></td></tr><tr><td width = '84' valign = 'top'> S N0.</td><td width = '539' valign = 'top'> DESCRIPTION </td><td width = '141' align = 'right' valign = 'top'> VALUE IN FORGEIGN  EX.</td><td width = '134' align = 'right' valign = 'top'> INR AMOUNT </td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'> 1 </td><td valign = 'top'><strong> BOX CIRCLE 2 * 2 * 0 WHITE </strong><br> (A32353) </td><td align = 'right' valign = 'top'><strong> USD 90 </strong></td><td align = 'right' valign = 'top'><strong> INR 6570 </strong></td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'> 2 </td><td valign = 'top'><strong> BOX CIRCLE 2 * 2 * 0 WHITE </strong><br> A32353)</td><td align = 'right' valign = 'top'><strong> USD 60 </strong></td><td align = 'right' valign = 'top'><strong> INR 5570 </strong></td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'>3</td><td valign = 'top'><strong> BOX CIRCLE 2 * 2 * 0 WHITE </strong><br> A32353)</td><td align = 'right' valign = 'top'><strong> USD 70 </strong></td><td align = 'right' align = 'top'><strong> INR 4570 </strong></td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'> 4 </td><td valign = 'top' ><strong> BOX CIRCLE 2 * 2 * 0 WHITE </strong><br> A32353)</td><td align = 'right' valign = 'top'><strong> USD 30 </strong></td><td align = 'right' valign = 'top'><strong> INR 6470 </strong></td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'> 5 </td> <td valign = 'top'><strong> BOX CIRCLE 2 * 2 * 0 WHITE </strong><br> A32353)</td><td align = 'right' valign = 'top'><strong> USD 90 </strong></td><td align = 'right' valign = 'top'><strong> INR 6570 </strong></td> </tr></tbody></table></td></tr><tr><td valign = 'top'  style = 'font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:left; font-style: normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding = '0' style = 'border:none'><tbody><tr><td colspan = '4' valign = 'top'  height = '10px'></td></tr><tr><td colspan = '4' valign = 'top'> Stand Details:</td></tr><tr><td width = '84' valign = 'top'> &nbsp;</td><td width = '487' valign = 'top'> &nbsp;</td><td width = '196' align = 'right' valign = 'top'><strong> SGST@.........%</ strong></td><td width = '131' align = 'right' valign = 'top'><strong> INR 555 </strong></td></tr><tr> <td valign = 'top'> &nbsp;</td><td valign = 'top'> &nbsp;</td><td align = 'right' valign = 'top'><strong> CGST@.........%</strong></td><td align = 'right' valign = 'top'><strong> 444 </strong></td></tr><tr><td valign = 'top'> &nbsp;</td><td valign = 'top'> &nbsp;</td><td align = 'right' valign = 'top'><strong> IGST@.........%</strong></td><td align = 'right' valign = 'top'><strong> 43434 </strong></td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'> &nbsp;</td><td valign = 'top'> &nbsp;</td><td align = 'right' valign = 'top'><strong> GROSS TOTAL </strong></td><td align = 'right' valign = 'top'><strong> INR 4570 </strong></td></tr><tr><td colspan = '4' valign = 'top'  height = '8px'></td></tr><tr><td valign = 'top'> &nbsp;</td> <td valign = 'top'> &nbsp;</td><td align = 'right' valign = 'top'><strong> PAID AMOUNT </strong></td><td align = 'right' valign = 'top'><strong> INR 6470 </strong></td></tr><tr><td colspan = '4' valign = 'top' height = '8px'></td></tr><tr><td valign = 'top'> &nbsp;</td><td valign = 'top'> &nbsp;</td><td align = 'right' valign = 'top'><strong> BALANCE TO BE PAID </strong></td><td align = 'right' valign = 'top' >< strong > INR 20000 </strong></td></tr></tbody></table></td></tr><tr><td valign = 'top' style = 'font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:left; font-style: normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding = '0' style = 'border:none'><tbody><tr><td width = '450'><strong> Paid by: Credit Card(Visa) </strong></td><td width = '450' align = 'right'><strong> &quot; Total sale value includes all applicable taxes & quot;</strong></td></tr></tbody></table></td></tr><tr><td valign = 'top'style = 'font-family: Arial, Helvetica, sans-serif; font-size: 13px; float:left; font-style: normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding = '0' style = 'border-top: solid 1px #333; border-right:none;border-left:none;border-bottom:none;'><tbody><tr><td width = '310' valign = 'top'> I the buyer hereby declare that the goods purchased by me will be taken out of the Indian Territory</td><td width = '396' align = 'right' valign = 'top' style = 'font-size: 20px;'> FOR AKBAR INTERNATIONAL</td></tr><tr><td valign = 'top'> &nbsp;</td><td align = 'right' valign = 'top'> signature </td></tr><tr><td valign = 'top'><strong> Buyer's Signature</strong></td><td align='right' valign='top'><strong>Sales executive's signature</strong></td></tr></tbody></table></td></tr><tr><td height = '5' valign = 'top'></td></tr></table></body></html>");

            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            //Set WebKit path
            settings.WebKitPath = Path.Combine(_hostingenv.ContentRootPath, "QtBinariesWindows");
            //Assign WebKit settings to HTML converter
            converter.ConverterSettings = settings;
            //Convert URL to PDF
            PdfDocument document = converter.Convert(htmlfile.ToString(), "");

            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close(true);

            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            fileStreamResult.FileDownloadName = "Sample.pdf";
            return fileStreamResult;
        }
    }
}
