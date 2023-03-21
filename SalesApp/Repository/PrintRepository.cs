using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Repository
{
    public class PrintRepository : IPrintRepository
    {
        private Sales_ERPContext _DBERP;
        private readonly ICommonRepository _comm;
        public PrintRepository(Sales_ERPContext dbcontext, ICommonRepository commonRepository)
        {

            this._DBERP = dbcontext;
            this._comm = commonRepository;

        }
        public PrintVM getAllOrders()
        {
            int userid = _comm.GetLoggedInUserId();
            PrintVM _pdetails = new PrintVM();

            _pdetails.Orders = (from master in this._DBERP.OrderMaster
                                join m in this._DBERP.OrderItemDetails
                                on master.Id equals m.OrderId
                                join c in this._DBERP.CustomerDetails
                                on m.OrderId equals c.OrderId into orderdetails
                                from order in orderdetails.Where(c => c.Isactive == true).DefaultIfEmpty()
                                join car in this._DBERP.CarpetNumber
                                on m.StockId equals car.TStockNo into carpet
                                from sc in carpet.DefaultIfEmpty()
                                join view in this._DBERP.V_FinishedItemDetail
                                on sc.item_finished_id equals view.ITEM_FINISHED_ID

                                where master.IsActive == true && m.IsActive == true && m.CreatedBy == userid && m.ItemType==1 && master.salestatus==1
                                select new itemprintdeatils
                                {
                                    orderid = m.OrderId,
                                    customername = order != null ? order.Name : "WebSale",
                                    stockdesc = view.ITEM_NAME + ',' + view.QUALITYNAME + ',' + view.SizeInch,
                                    stockvalue = m.PriceInr,
                                    unit = m.Unit,
                                    prefix = m.OrderTypePrefix,
                                    billdesc= m.OrderTypePrefix+"/"+m.Unit+"/"+m.BillId,
                                    billid=m.BillId,
                                    odate=m.CreatedDatetime,
                                    itemorderid=m.Id,
                                   
                                    
                                    


                                }).ToList().GroupBy(m => m.orderid)
    .Select(g => new itemprintdeatils
    {
        orderid = g.FirstOrDefault().orderid,
        customername = g.FirstOrDefault().customername,
     //   stockvalue=g.Sum(a=>a.stockvalue),
        stockvalue = g.GroupBy(a => a.itemorderid).Select(x => new { x.Key, totalval = x.FirstOrDefault().stockvalue }).Sum(a => a.totalval),
        desc = g.Select(a=>a.stockdesc).ToList(),
        bills=g.Where(a=>a.billid>0).Select(a=>a.billdesc).Distinct().ToList(),
        unit= g.FirstOrDefault().unit,
        odate=g.FirstOrDefault().odate
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).OrderByDescending(a => a.odate).ToList();

            return _pdetails;
        }
    }
}
