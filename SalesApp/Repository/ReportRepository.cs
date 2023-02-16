using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Globalization;
using Microsoft.Extensions.Configuration;
namespace SalesApp.Repository
{
    public class ReportRepository : IReportRepository
    {
        private Sales_ERPContext _DBERP;
        private ExportErpDbContext _SALESRP;
        ICommonRepository _comm;

        public ReportRepository(Sales_ERPContext salesdbcontext, ICommonRepository comm,ExportErpDbContext expdbcontext)
        {   

            this._DBERP = salesdbcontext;
            this._SALESRP = expdbcontext;
            this._comm = comm;
            

        }
        public async Task<List<ReportVM>> getAllReportDetail(ReportVM pcm)
        {
            List<ReportVM> _finaldata = new List<ReportVM>();
            var reportdata = await (from item in this._DBERP.OrderItemDetails
                                    join pay in this._DBERP.OrderPayment.Where(a=>a.IsActive==true)
                                    on item.OrderId equals pay.OrderId
                                    //   from item in itemmaster.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join pl in this._DBERP.PoolMaster
                                    //   on m.PoolId equals pl.Id into pool
                                    //   from pm in pool.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join c in this._DBERP.AgentContact
                                    //   on ma.AgentId equals c.AgentId into contactdetails
                                    //   from d in contactdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                    //   join v in this._DBERP.VehicleDetails
                                    //   on ma.AgentId equals v.AgentId into vehicledetails
                                    //   from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join vm in this._DBERP.VehicleMaster
                                    //  on vh.VehicleId equals vm.Id into vehiclemaster
                                    //   from vmaster in vehiclemaster.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join l in this._DBERP.LanguagesMaster
                                    //on m.LanguageId equals l.Id into language
                                    //   from la in language.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join c in this._DBERP.CountriesMaster
                                    //  on m.CountryId equals c.Id into country
                                    //   from cu in country.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join s in this._DBERP.SeriesMaster
                                    //  on m.SeriesId equals s.Id into series
                                    //   from se in series.Where(f => f.IsActive == true).DefaultIfEmpty()
                                    //   join ur in this._DBERP.UserLogin
                                    //    on ma.AgentId equals ur.Id into ul
                                    //   from ulmaster in ul.Where(f => f.IsActive == true).DefaultIfEmpty()

                                    where item.IsActive == true  && ((pcm.fromdate == null ? item.CreatedDatetime.Value.Date > DateTime.Today : item.CreatedDatetime.Value.Date >= pcm.fromdate.Date && item.CreatedDatetime.Value.Date <= pcm.Todate.Date) || (item.CreatedDatetime.Value.Date >= pcm.fromdate.Date && item.CreatedDatetime.Value.Date <= pcm.fromdate.Date))

                              select new ReportVM
                              {
                                  BillId = item.BillId,
                                  paymode = pay.PayMode,
                                  SaleValue = pay.AmoutHd,
                                  fromdate=item.CreatedDatetime.Value.Date

                              }).ToListAsync();

            
                int index = 1;
            _finaldata = reportdata.Distinct().GroupBy(c => new { c.BillId, c.paymode, c.SaleValue }).Distinct().Select(g => new ReportVM
            {
                srno = index++,
                BillId = g.FirstOrDefault().BillId,
                paymode = g.FirstOrDefault().paymode,
                SaleValue=g.FirstOrDefault().SaleValue,
                fromdate=g.FirstOrDefault().fromdate


            }).ToList();
            return _finaldata;
        }

        public async Task<List<ReportVM>> getitemcostingReportDetail(ReportVM pcm)
        {
            List<ReportVM> _finaldata = new List<ReportVM>();
            try
            {

          
            var reportdata = await (from item in this._DBERP.OrderItemDetails
                                    join order in this._DBERP.OrderMaster.Where(a=>a.IsActive==true)
                                    on item.OrderId equals order.Id
                                    join pay in this._DBERP.OrderPayment.Where(a => a.IsActive == true)
                                    on item.OrderId equals pay.OrderId
                                    join stock in this._DBERP.CarpetNumber
                                    on item.StockId equals stock.TStockNo
                                    join vf in this._DBERP.V_FinishedItemDetail
                                    on stock.item_finished_id equals vf.ITEM_FINISHED_ID


                                    where item.IsActive == true && ((pcm.fromdate == null ? item.CreatedDatetime.Value.Date > DateTime.Today : item.CreatedDatetime.Value.Date >= pcm.fromdate.Date && item.CreatedDatetime.Value.Date <= pcm.Todate.Date) || (item.CreatedDatetime.Value.Date >= pcm.fromdate.Date && item.CreatedDatetime.Value.Date <= pcm.fromdate.Date))

                                    select new ReportVM
                                    {
                                        saledate = order.SaleDate,
                                        invno = item.OrderTypePrefix + "/" + Convert.ToString(item.Unit) + "/" + item.BillId.ToString(),
                                        stockno = stock.TStockNo??String.Empty,
                                        itemdesc = vf.QUALITYNAME ?? String.Empty,
                                        size = vf.SizeInch ?? String.Empty,
                                        qty = 1,
                                        purchasecost = 0,
                                        SaleValue = item.Price??0,
                                        itemgst = 0,
                                        salegst = 5,
                                        basicamount = stock.Price??0,
                                        discount = order.Discountper??0,
                                        payamount = pay.AmoutHd??0,
                                        orderid = order.Id,
                                        itemid = item.Id,
                                        paymentid = pay.Id,
                                        BillId = item.BillId,
                                        paymode = pay.PayMode??0,


                                        fromdate = item.CreatedDatetime.Value.Date

                                    }).ToListAsync();


            int index = 1;
            _finaldata = reportdata.Distinct().GroupBy(c => new { c.itemid }).Distinct().Select(g => new ReportVM
            {
                srno = index++,
                saledate=g.FirstOrDefault().saledate,
                invno=g.FirstOrDefault().invno,
                stockno=g.FirstOrDefault().stockno,
                itemdesc = g.FirstOrDefault().itemdesc,
                size=g.FirstOrDefault().size,
                qty=g.FirstOrDefault().qty,
                purchasecost= this._SALESRP.CarpetNumber.Where(a => a.TStockNo == g.FirstOrDefault().stockno).Select(b=>b.Price).FirstOrDefault()??0,
                //purchasecost = g.FirstOrDefault().purchasecost,
                SaleValue = g.FirstOrDefault().SaleValue,
                itemgst=g.FirstOrDefault().itemgst,
                salegst=g.FirstOrDefault().salegst,
                basicamount= (decimal)(g.FirstOrDefault().SaleValue*100)/(g.FirstOrDefault().salegst+100),
                discount=g.FirstOrDefault().discount,
                actualbasicamount=(decimal)(g.FirstOrDefault().payamount*100)/(g.FirstOrDefault().salegst+100),
                payamount=g.FirstOrDefault().payamount,
                BillId = g.FirstOrDefault().BillId,
                paymode = g.FirstOrDefault().paymode,
                fromdate = g.FirstOrDefault().fromdate,
                orderid=g.FirstOrDefault().orderid,
                itemid=g.FirstOrDefault().itemid,
                paymentid=g.FirstOrDefault().paymentid


            }).OrderBy(a=>a.orderid).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _finaldata;
        }

        public Task<ReportVM> Init_Report()
        {
            throw new System.NotImplementedException();
            //ReportVM _data = new ReportVM();
            //_data.fromdate=DateTimeExtensions.
            //throw new System.NotImplementedException();
        }
    }
}
