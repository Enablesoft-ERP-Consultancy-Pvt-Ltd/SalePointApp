using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesApp.ViewModel;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
namespace SalesApp.Controllers
{
    public class ReportController : Controller
    {
        IReportRepository _prnt;
       
        private readonly ICommonRepository _comm;
        IWebHostEnvironment _hostingenv;
        IConfiguration Configuration;
        IReportRepository _report;
        public ReportController(IReportRepository _rpt, IWebHostEnvironment _env, ICommonRepository commonRepository, IConfiguration _config)
        {
            this._report = _rpt;
          //  this._DBERP = dbcontext;
            this._hostingenv = _env;
            this._comm = commonRepository;
            this.Configuration = _config;

        }
        public async Task<IActionResult> Index()
        {
            ReportVM _result = new ReportVM();
           // _result = await _report.Init_Report();
            _result.fromdate = DateTime.Today;
            _result.Todate = DateTime.Today;
            return View("Index", _result);
        }
        public async Task<IActionResult> ItemCostingInit()
        {
            ReportVM _result = new ReportVM();
            // _result = await _report.Init_Report();
            _result.fromdate = DateTime.Today;
            _result.Todate = DateTime.Today;
            return View("ICIndex", _result);
        }
        public async Task<IActionResult> DownloadDailyCollectionReport(string agentid, string Name, DateTime fromdate, DateTime Todate, int paystatusid)
        {
            string filepathtoreturn = String.Empty;// _hostingEnvironment.WebRootPath;
            string sFileName = string.Empty;
            string sWebRootFolder = string.Empty;// _hostingenv.WebRootPath;
            string FileName = string.Empty;
            int maxRows = 10;
            //  int CurrentPageIndex = 1;.
            ReportVM _alluser = new ReportVM();
            //if (!string.IsNullOrEmpty(Name))
            //{
            //    _alluser.AgentId = Convert.ToInt16(agentid);
            //}
            //else
            //{ _alluser.AgentId = 0; }


            _alluser.fromdate = fromdate;
            _alluser.Todate = Todate;
          //  _alluser.paystatusid = paystatusid;

            var memory = new MemoryStream();
            List<ReportVM> _reportdata = new List<ReportVM>();
            sWebRootFolder = _hostingenv.WebRootPath + $@"\Reports";
            try
            {
                FileName = "Internal_Report";
                _reportdata = await _report.getAllReportDetail(_alluser);
                //_reportdata[0] .fromdate = fromdate;
                //_reportdata[0].Todate = Todate;
                //  _data.paystatusid = paystatusid;
                GenerateDailyCollectionReport(sWebRootFolder, FileName, _reportdata,fromdate,Todate);
            }
            catch (Exception)
            {

                return NoContent();
            }

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, FileName) + ".xlsx", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);


        }

        public async Task<IActionResult> DownloadItemCostingReport(string agentid, string Name, DateTime fromdate, DateTime Todate, int paystatusid)
        {
            string filepathtoreturn = String.Empty;// _hostingEnvironment.WebRootPath;
            string sFileName = string.Empty;
            string sWebRootFolder = string.Empty;// _hostingenv.WebRootPath;
            string FileName = string.Empty;
            int maxRows = 10;
            //  int CurrentPageIndex = 1;.
            ReportVM _alluser = new ReportVM();
            //if (!string.IsNullOrEmpty(Name))
            //{
            //    _alluser.AgentId = Convert.ToInt16(agentid);
            //}
            //else
            //{ _alluser.AgentId = 0; }


            _alluser.fromdate = fromdate;
            _alluser.Todate = Todate;
            //  _alluser.paystatusid = paystatusid;

            var memory = new MemoryStream();
            List<ReportVM> _reportdata = new List<ReportVM>();
            sWebRootFolder = _hostingenv.WebRootPath + $@"\Reports";
            try
            {
                FileName = "Item_Costing";
                _reportdata = await _report.getitemcostingReportDetail(_alluser);
                //_reportdata[0] .fromdate = fromdate;
                //_reportdata[0].Todate = Todate;
                //  _data.paystatusid = paystatusid;
                GenerateItemCostingReport(sWebRootFolder, FileName, _reportdata, fromdate, Todate);
            }
            catch (Exception)
            {

                return NoContent();
            }

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, FileName) + ".xlsx", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);


        }
        public void GenerateDailyCollectionReport(string sWebRootFolder, string FileName, List<ReportVM> _data,DateTime _fromdate,DateTime _todate)
        {
            if (_data.Count > 0)
            {
                string filepathtoreturn = String.Empty;// _hostingEnvironment.WebRootPath;
                string sFileName = string.Empty;
                var xapp = new XLWorkbook();
                var sht = xapp.Worksheets.Add("Sale Data");
              //  int row = 7;
                sht.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                sht.PageSetup.AdjustTo(75);
                sht.PageSetup.FitToPages(1,1);
                sht.PageSetup.PaperSize = XLPaperSize.A4Paper;
                //sht.PageSetup.VerticalDpi = 300;
                //sht.PageSetup.HorizontalDpi = 300;
                sht.Column("A").Width = 30;
                sht.Column("B").Width = 20;
                sht.Column("C").Width = 40;
                sht.Column("D").Width = 40;

                //sht.Cell("F1").Value = "From Date";
                //sht.Range("F1:G1").Style.Font.FontName = "Tahoma";
                //sht.Range("F1:G1").Style.Font.FontSize = 14;
                //sht.Range("F1:G1").Style.Font.Bold = true;
                //sht.Range("F1:G1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("F1:G1").Merge();
                //sht.Cell("H1").Value = "To Date";
                //sht.Range("H1:I1").Style.Font.FontName = "Tahoma";
                //sht.Range("H1:I1").Style.Font.FontSize = 14;
                //sht.Range("H1:I1").Style.Font.Bold = true;
                //sht.Range("H1:I1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("H1:I1").Merge();
                //sht.Cell("J1").Value = "Report Printing Date";
                //sht.Range("J1:K1").Style.Font.FontName = "Tahoma";
                //sht.Range("J1:K1").Style.Font.FontSize = 14;
                //sht.Range("J1:K1").Style.Font.Bold = true;
                //sht.Range("J1:K1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("J1:K1").Merge();
                sht.Cell("A1").Value = "DAILY COLLECTION REPORT";
                sht.Range("A1:D1").Style.Font.FontName = "Tahoma";
                sht.Range("A1:D1").Style.Font.FontSize = 16;
                sht.Range("A1:D1").Style.Font.Bold = true;
                sht.Range("A1:D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A1:D1").Merge();
                sht.Cell("A2").Value = "DAY & DATE";
                sht.Range("A2:A2").Style.Font.FontName = "Tahoma";
                sht.Range("A2:A2").Style.Font.FontSize = 12;
                sht.Range("A2:A2").Style.Font.Bold = true;
                sht.Range("A2:A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A2:A2").Merge();
                sht.Cell("B2").Value = "INVOICE NO.";
                sht.Range("B2:B2").Style.Font.FontName = "Tahoma";
                sht.Range("B2:B2").Style.Font.FontSize = 12;
                sht.Range("B2:B2").Style.Font.Bold = true;
                sht.Range("B2:B2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("B2:B2").Merge();
                sht.Cell("C2").Value = "BANK PAYMENT RECEIVED";
                sht.Range("C2:C2").Style.Font.FontName = "Tahoma";
                sht.Range("C2:C2").Style.Font.FontSize = 12;
                sht.Range("C2:C2").Style.Font.Bold = true;
                sht.Range("C2:C2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("C2:C2").Merge();
                sht.Cell("D2").Value = "CASH PAYMENT RECEIVED";
                sht.Range("D2:D2").Style.Font.FontName = "Tahoma";
                sht.Range("D2:D2").Style.Font.FontSize = 12;
                sht.Range("D2:D2").Style.Font.Bold = true;
                sht.Range("D2:D2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("D2:D2").Merge();
               
                using (var a = sht.Range("A2:D2"))
                {
                    a.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                }
               
               
                int row = 3;
                
                int parchitotal = 0, cntsaleyes = 0, cntsaleno = 0;
                decimal? CASH_VALUE = 0, BANK_VALUE = 0, TOTALPAYMENT = 0;
                foreach (var item in _data)
                {
                    sht.Cell("A" + row).Value = item.fromdate;
                    sht.Range("A" + row + ":A" + row).Style.Font.FontName = "Tahoma";
                    sht.Range("A" + row + ":A" + row).Style.Font.FontSize = 12;
                    //sht.Range("A" + row + ":A" + row).Style.Font.Bold = true;
                    sht.Range("A" + row + ":A" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("A" + row + ":A" + row).Merge();
                    sht.Cell("B" + row).Value = item.BillId;
                    sht.Range("B" + row + ":B" + row).Style.Font.FontName = "Tahoma";
                    sht.Range("B" + row + ":B" + row).Style.Font.FontSize = 12;
                    //sht.Range("B" + row + ":B" + row).Style.Font.Bold = true;
                    sht.Range("B" + row + ":B" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("B" + row + ":B" + row).Merge();
                    if (item.paymode != 1)
                    {
                        BANK_VALUE += item.SaleValue;
                        sht.Cell("C" + row).Value = item.SaleValue;
                        sht.Range("C" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                        sht.Range("C" + row + ":C" + row).Style.Font.FontSize = 12;
                      //  sht.Range("C" + row + ":C" + row).Style.Font.Bold = true;
                        sht.Range("C" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        sht.Range("C" + row + ":C" + row).Merge();
                    }
                    else
                    {
                        sht.Cell("C" + row).Value = 0;
                        sht.Range("C" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                        sht.Range("C" + row + ":C" + row).Style.Font.FontSize = 12;
                        //sht.Range("C" + row + ":C" + row).Style.Font.Bold = true;
                        sht.Range("C" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        sht.Range("C" + row + ":C" + row).Merge();
                    }
                    if (item.paymode == 1)
                    {
                        CASH_VALUE += item.SaleValue;
                        sht.Cell("D" + row).Value = item.SaleValue;
                        sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                        sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                        //sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                        sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        sht.Range("D" + row + ":D" + row).Merge();
                    }
                    else
                    {
                        sht.Cell("D" + row).Value = 0;
                        sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                        sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                        //sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                        sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        sht.Range("D" + row + ":D" + row).Merge();


                    }
                    using (var a = sht.Range("A" + row + ":D" + row))
                    {
                        a.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        a.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        a.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        a.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    }
                  
                    row += 1;
                }
                TOTALPAYMENT += CASH_VALUE + BANK_VALUE;
                using (var a = sht.Range("A" + row + ":D" + (row + 3)))
                {
                    a.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                }
                row += 1;
                sht.Cell("A" + row).Value = "Total Bank Payment";
                sht.Range("A" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                sht.Range("A" + row + ":C" + row).Style.Font.FontSize = 12;
                sht.Range("A" + row + ":C" + row).Style.Font.Bold = true;
                sht.Range("A" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A" + row + ":C" + row).Merge();
                sht.Cell("D" + row).Value = BANK_VALUE;
                sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("D" + row + ":D" + row).Merge();
                row += 1;
                sht.Cell("A" + row).Value = "Total Cash Payment";
                sht.Range("A" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                sht.Range("A" + row + ":C" + row).Style.Font.FontSize = 12;
                sht.Range("A" + row + ":C" + row).Style.Font.Bold = true;
                sht.Range("A" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A" + row + ":C" + row).Merge();
                sht.Cell("D" + row).Value = CASH_VALUE;
                sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("D" + row + ":D" + row).Merge();
                row += 1;
                sht.Cell("A" + row).Value = "Grand Total";
                sht.Range("A" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                sht.Range("A" + row + ":C" + row).Style.Font.FontSize = 12;
                sht.Range("A" + row + ":C" + row).Style.Font.Bold = true;
                sht.Range("A" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A" + row + ":C" + row).Merge();
                sht.Cell("D" + row).Value = TOTALPAYMENT;
                sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("D" + row + ":D" + row).Merge();
              
             
                string Fileextension = "xlsx";
                
                string sfilename = Path.Combine(sWebRootFolder, FileName) + "." + Fileextension;
                using (MemoryStream stream = new MemoryStream())
                {
                    xapp.SaveAs(sfilename);
                    //Return xlsx Excel File  
                    // File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                }
            }


        }
        public void GenerateItemCostingReport(string sWebRootFolder, string FileName, List<ReportVM> _data, DateTime _fromdate, DateTime _todate)
        {
            if (_data.Count > 0)
            {
                string filepathtoreturn = String.Empty;// _hostingEnvironment.WebRootPath;
                string sFileName = string.Empty;
                var xapp = new XLWorkbook();
                var sht = xapp.Worksheets.Add("Item Costing");
                //  int row = 7;
                sht.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                sht.PageSetup.AdjustTo(75);
                sht.PageSetup.FitToPages(1, 1);
                sht.PageSetup.PaperSize = XLPaperSize.A4Paper;
                //sht.PageSetup.VerticalDpi = 300;
                //sht.PageSetup.HorizontalDpi = 300;
                sht.Column("A").Width = 30;
                sht.Column("B").Width = 20;
                sht.Column("C").Width = 40;
                sht.Column("D").Width = 40;
                sht.Column("E").Width = 30;
                sht.Column("F").Width = 20;
                sht.Column("G").Width = 40;
                sht.Column("H").Width = 40;
                sht.Column("I").Width = 30;
                sht.Column("J").Width = 20;
                sht.Column("K").Width = 40;
                sht.Column("L").Width = 40;
                sht.Column("M").Width = 40;


                sht.Cell("A1").Value = "ITEM WISE COSTING REPORT (Purchases, Sale & Received Payment Details)";
                sht.Range("A1:M1").Style.Font.FontName = "Tahoma";
                sht.Range("A1:M1").Style.Font.FontSize = 16;
                sht.Range("A1:M1").Style.Font.Bold = true;
                sht.Range("A1:M1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A1:M1").Merge();
                sht.Cell("A2").Value = "INVOICE DATE";
                sht.Range("A2:A2").Style.Font.FontName = "Tahoma";
                sht.Range("A2:A2").Style.Font.FontSize = 11;
                sht.Range("A2:A2").Style.Font.Bold = true;
                sht.Range("A2:A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("A2:A2").Merge();
                sht.Cell("B2").Value = "INVOICE NO.";
                sht.Range("B2:B2").Style.Font.FontName = "Tahoma";
                sht.Range("B2:B2").Style.Font.FontSize = 11;
                sht.Range("B2:B2").Style.Font.Bold = true;
                sht.Range("B2:B2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("B2:B2").Merge();
                sht.Cell("C2").Value = "ITEM CODE";
                sht.Range("C2:C2").Style.Font.FontName = "Tahoma";
                sht.Range("C2:C2").Style.Font.FontSize = 11;
                sht.Range("C2:C2").Style.Font.Bold = true;
                sht.Range("C2:C2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("C2:C2").Merge();
                sht.Cell("D2").Value = "COLLECTION";
                sht.Range("D2:D2").Style.Font.FontName = "Tahoma";
                sht.Range("D2:D2").Style.Font.FontSize = 11;
                sht.Range("D2:D2").Style.Font.Bold = true;
                sht.Range("D2:D2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("D2:D2").Merge();
                sht.Cell("E2").Value = "SIZE";
                sht.Range("E2:E2").Style.Font.FontName = "Tahoma";
                sht.Range("E2:E2").Style.Font.FontSize = 11;
                sht.Range("E2:E2").Style.Font.Bold = true;
                sht.Range("E2:E2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("E2:E2").Merge();
                sht.Cell("F2").Value = "QTY";
                sht.Range("F2:F2").Style.Font.FontName = "Tahoma";
                sht.Range("F2:F2").Style.Font.FontSize = 11;
                sht.Range("F2:F2").Style.Font.Bold = true;
                sht.Range("F2:F2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("F2:F2").Merge();
                sht.Cell("G2").Value = "NET PURCHASES PRICE (INCL. OF GST)";
                sht.Range("G2:G2").Style.Font.FontName = "Tahoma";
                sht.Range("G2:G2").Style.Font.FontSize = 11;
                sht.Range("G2:G2").Style.Font.Bold = true;
                sht.Range("A2:M2").Style.Alignment.WrapText = true;
                sht.Range("G2:G2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("G2:G2").Merge();
                sht.Cell("H2").Value = "SALE MRP (INCL OF GST)";
                sht.Range("H2:H2").Style.Font.FontName = "Tahoma";
                sht.Range("H2:H2").Style.Font.FontSize = 11;
                sht.Range("H2:H2").Style.Font.Bold = true;
                sht.Range("H2:H2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("H2:H2").Merge();
                sht.Cell("I2").Value = "GST%";
                sht.Range("I2:I2").Style.Font.FontName = "Tahoma";
                sht.Range("I2:I2").Style.Font.FontSize = 11;
                sht.Range("I2:I2").Style.Font.Bold = true;
                sht.Range("I2:I2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("I2:I2").Merge();
                sht.Cell("J2").Value = "BASIC AMOUNT";
                sht.Range("J2:J2").Style.Font.FontName = "Tahoma";
                sht.Range("J2:J2").Style.Font.FontSize = 11;
                sht.Range("J2:J2").Style.Font.Bold = true;
                sht.Range("J2:J2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("J2:J2").Merge();
                sht.Cell("K2").Value = "DISCOUNT %";
                sht.Range("K2:K2").Style.Font.FontName = "Tahoma";
                sht.Range("K2:K2").Style.Font.FontSize = 11;
                sht.Range("K2:K2").Style.Font.Bold = true;
                sht.Range("K2:K2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("K2:K2").Merge();
                sht.Cell("L2").Value = "AMOUNT PAID BY CUSTOMER";
                sht.Range("L2:L2").Style.Font.FontName = "Tahoma";
                sht.Range("L2:L2").Style.Font.FontSize = 11;
                sht.Range("L2:L2").Style.Font.Bold = true;
                sht.Range("L2:L2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("L2:L2").Merge();
                sht.Cell("M2").Value = "ACTUAL BASIC AMOUNT";
                sht.Range("M2:M2").Style.Font.FontName = "Tahoma";
                sht.Range("M2:M2").Style.Font.FontSize = 11;
                sht.Range("M2:M2").Style.Font.Bold = true;
                sht.Range("M2:M2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("M2:M2").Merge();

                using (var a = sht.Range("A2:M2"))
                {
                    a.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                }


                int row = 3;

                int QTY = 0, cntsaleyes = 0, cntsaleno = 0;
                Int64 orderid = 0;
                decimal? PURCHASE_VALUE = 0, MRP_VALUE = 0, BASIC_AMOUNT = 0,AMOUNT_PAID=0,ACTUAL_BASIC=0;
                foreach (var item in _data)
                {
                    sht.Cell("A" + row).Value = item.saledate;
                    sht.Range("A" + row + ":A" + row).Style.Font.FontName = "Tahoma";
                    sht.Range("A" + row + ":A" + row).Style.Font.FontSize = 10;
                    //sht.Range("A" + row + ":A" + row).Style.Font.Bold = true;
                    sht.Range("A" + row + ":A" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("A" + row + ":A" + row).Merge();
                    sht.Cell("B" + row).Value = item.invno;
                    sht.Range("B" + row + ":B" + row).Style.Font.FontName = "Tahoma";
                    sht.Range("B" + row + ":B" + row).Style.Font.FontSize = 10;
                    //sht.Range("B" + row + ":B" + row).Style.Font.Bold = true;
                    sht.Range("B" + row + ":B" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("B" + row + ":B" + row).Merge();
                    sht.Cell("C" + row).Value = item.stockno;
                    sht.Range("C" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                    sht.Range("C" + row + ":C" + row).Style.Font.FontSize = 10;
                    //sht.Range("C" + row + ":C" + row).Style.Font.Bold = true;
                    sht.Range("C" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("C" + row + ":C" + row).Merge();
                    sht.Cell("D"+row).Value = item.itemdesc;
                    sht.Range("D"+row+":D"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("D"+row+":D"+row).Style.Font.FontSize = 10;
                    //sht.Range("D"+row+":D"+row).Style.Font.Bold = true;
                    sht.Range("D"+row+":D"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("D"+row+":D"+row).Merge();
                    sht.Cell("E"+row).Value = item.size;
                    sht.Range("E"+row+":E"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("E"+row+":E"+row).Style.Font.FontSize = 10;
                    //sht.Range("E"+row+":E"+row).Style.Font.Bold = true;
                    sht.Range("E"+row+":E"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("E"+row+":E"+row).Merge();
                    sht.Cell("F"+row).Value = item.qty;
                    sht.Range("F"+row+":F"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("F"+row+":F"+row).Style.Font.FontSize = 10;
                    //sht.Range("F"+row+":F"+row).Style.Font.Bold = true;
                    sht.Range("F"+row+":F"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("F"+row+":F"+row).Merge();
                    sht.Cell("G"+row).Value = item.purchasecost;
                    sht.Range("G"+row+":G"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("G"+row+":G"+row).Style.Font.FontSize = 10;
                    //sht.Range("G"+row+":G"+row).Style.Font.Bold = true;
                    sht.Range("G"+row+":G"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("G"+row+":G"+row).Merge();
                    sht.Cell("H"+row).Value = item.SaleValue;
                    sht.Range("H"+row+":H"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("H"+row+":H"+row).Style.Font.FontSize = 10;
                    //sht.Range("H"+row+":H"+row).Style.Font.Bold = true;
                    sht.Range("H"+row+":H"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("H"+row+":H"+row).Merge();
                    sht.Cell("I"+row).Value = item.salegst;
                    sht.Range("I"+row+":I"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("I"+row+":I"+row).Style.Font.FontSize = 10;
                    //sht.Range("I"+row+":I"+row).Style.Font.Bold = true;
                    sht.Range("I"+row+":I"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("I"+row+":I"+row).Merge();
                    sht.Cell("J"+row).Value = item.basicamount;
                    sht.Range("J"+row+":J"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("J"+row+":J"+row).Style.Font.FontSize = 10;
                    //sht.Range("J"+row+":J"+row).Style.Font.Bold = true;
                    sht.Range("J" + row + ":J" + row).Style.NumberFormat.Format = "####0.00";
                    sht.Range("J"+row+":J"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("J"+row+":J"+row).Merge();
                    sht.Cell("K"+row).Value = item.discount;
                    sht.Range("K"+row+":K"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("K"+row+":K"+row).Style.Font.FontSize = 10;
                    //sht.Range("K"+row+":K"+row).Style.Font.Bold = true;
                    sht.Range("K"+row+":K"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("K"+row+":K"+row).Merge();
                    sht.Cell("L" + row).Value = orderid != item.orderid?item.payamount:0;
                    sht.Range("L" + row + ":L" + row).Style.Font.FontName = "Tahoma";
                    sht.Range("L" + row + ":L" + row).Style.Font.FontSize = 10;
                    //sht.Range("L"+row+":L"+row).Style.Font.Bold = true;
                    sht.Range("L" + row + ":L" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("L" + row + ":L" + row).Merge();
                   
                    sht.Cell("M"+row).Value = orderid != item.orderid ? item.actualbasicamount : 0 ;
                    sht.Range("M"+row+":M"+row).Style.Font.FontName = "Tahoma";
                    sht.Range("M"+row+":M"+row).Style.Font.FontSize = 10;
                    //sht.Range("M"+row+":M"+row).Style.Font.Bold = true;
                    sht.Range("M" + row + ":M" + row).Style.NumberFormat.Format = "####0.00";
                    sht.Range("M"+row+":M"+row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    sht.Range("M"+row+":M"+row).Merge();
                    using (var a = sht.Range("A" + row + ":M" + row))
                    {
                        a.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        a.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        a.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        a.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    }
                    PURCHASE_VALUE += item.netpurchasecost;
                    MRP_VALUE += item.SaleValue;
                    BASIC_AMOUNT += item.basicamount;

                    AMOUNT_PAID += orderid != item.orderid ? item.payamount : 0;
                    ACTUAL_BASIC += orderid != item.orderid ? item.actualbasicamount : 0;
                    QTY += 1;

                    row += 1;
                    orderid = item.orderid;
                }
               
                using (var a = sht.Range("A" + row + ":M" + (row + 3)))
                {
                    a.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    a.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                }
                row += 1;
                sht.Cell("E" + row).Value = "Total";
                sht.Range("E" + row + ":E" + row).Style.Font.FontName = "Tahoma";
                sht.Range("E" + row + ":E" + row).Style.Font.FontSize = 12;
                sht.Range("E" + row + ":E" + row).Style.Font.Bold = true;
                sht.Range("E" + row + ":E" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("E" + row + ":E" + row).Merge();
                sht.Cell("F" + row).Value = QTY;
                sht.Range("F" + row + ":F" + row).Style.Font.FontName = "Tahoma";
                sht.Range("F" + row + ":F" + row).Style.Font.FontSize = 12;
                sht.Range("F" + row + ":F" + row).Style.Font.Bold = true;
                sht.Range("F" + row + ":F" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("F" + row + ":F" + row).Merge();
                sht.Cell("G" + row).Value = PURCHASE_VALUE;
                sht.Range("G" + row + ":G" + row).Style.Font.FontName = "Tahoma";
                sht.Range("G" + row + ":G" + row).Style.Font.FontSize = 12;
                sht.Range("G" + row + ":G" + row).Style.Font.Bold = true;
                sht.Range("G" + row + ":G" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("G" + row + ":G" + row).Merge();
                sht.Cell("H" + row).Value = MRP_VALUE;
                sht.Range("H" + row + ":H" + row).Style.Font.FontName = "Tahoma";
                sht.Range("H" + row + ":H" + row).Style.Font.FontSize = 12;
                sht.Range("H" + row + ":H" + row).Style.Font.Bold = true;
                sht.Range("H" + row + ":H" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("H" + row + ":H" + row).Merge();
                sht.Cell("J" + row).Value = BASIC_AMOUNT;
                sht.Range("J" + row + ":J" + row).Style.Font.FontName = "Tahoma";
                sht.Range("J" + row + ":J" + row).Style.Font.FontSize = 12;
                sht.Range("J" + row + ":J" + row).Style.Font.Bold = true;
                sht.Range("J" + row + ":J" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("J" + row + ":J" + row).Merge();
                sht.Cell("L" + row).Value = AMOUNT_PAID;
                sht.Range("L" + row + ":L" + row).Style.Font.FontName = "Tahoma";
                sht.Range("L" + row + ":L" + row).Style.Font.FontSize = 12;
                sht.Range("L" + row + ":L" + row).Style.Font.Bold = true;
                sht.Range("L" + row + ":L" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("L" + row + ":L" + row).Merge();
                sht.Cell("M" + row).Value = ACTUAL_BASIC;
                sht.Range("M" + row + ":M" + row).Style.Font.FontName = "Tahoma";
                sht.Range("M" + row + ":M" + row).Style.Font.FontSize = 12;
                sht.Range("M" + row + ":M" + row).Style.Font.Bold = true;
                sht.Range("M" + row + ":M" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                sht.Range("M" + row + ":M" + row).Merge();
                //row += 1;
                //sht.Cell("A" + row).Value = "Total Cash Payment";
                //sht.Range("A" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                //sht.Range("A" + row + ":C" + row).Style.Font.FontSize = 12;
                //sht.Range("A" + row + ":C" + row).Style.Font.Bold = true;
                //sht.Range("A" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("A" + row + ":C" + row).Merge();
                //sht.Cell("D" + row).Value = CASH_VALUE;
                //sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                //sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                //sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                //sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("D" + row + ":D" + row).Merge();
                //row += 1;
                //sht.Cell("A" + row).Value = "Grand Total";
                //sht.Range("A" + row + ":C" + row).Style.Font.FontName = "Tahoma";
                //sht.Range("A" + row + ":C" + row).Style.Font.FontSize = 12;
                //sht.Range("A" + row + ":C" + row).Style.Font.Bold = true;
                //sht.Range("A" + row + ":C" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("A" + row + ":C" + row).Merge();
                //sht.Cell("D" + row).Value = TOTALPAYMENT;
                //sht.Range("D" + row + ":D" + row).Style.Font.FontName = "Tahoma";
                //sht.Range("D" + row + ":D" + row).Style.Font.FontSize = 12;
                //sht.Range("D" + row + ":D" + row).Style.Font.Bold = true;
                //sht.Range("D" + row + ":D" + row).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //sht.Range("D" + row + ":D" + row).Merge();



                string Fileextension = "xlsx";
               
                string sfilename = Path.Combine(sWebRootFolder, FileName) + "." + Fileextension;
                using (MemoryStream stream = new MemoryStream())
                {
                    xapp.SaveAs(sfilename);
                    //Return xlsx Excel File  
                    // File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                }
            }


        }
    }
}
