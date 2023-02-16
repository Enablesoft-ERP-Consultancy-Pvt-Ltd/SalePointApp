using System;
using System.Collections.Generic;
using System.Linq;
using SalesApp.ViewModel;
using SalesApp.Repository.Interface;
using SALEERP.Data;
using SalesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SalesApp.Repository
{
    public class MirrorRepository : IMirrorRepository
    {
        private Sales_ERPContext _DBERP;
      
        public MirrorRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }

        public async Task<MirrorDetailsVM> getAllMirrors()
        {
            MirrorDetailsVM _mirror = new MirrorDetailsVM();

           
            List<MirrorDetailVM> _allmirrordetails = new List<MirrorDetailVM>();

            List<Totalmirrorandsaledetails> _salelocal = await(from m in this._DBERP.MirrorDetails
                                                               join om in this._DBERP.OrderMaster
                                                               on m.Id equals om.MirrorId into ordermaster
                                                               from master in ordermaster.Where(f => f.IsActive == true).DefaultIfEmpty()
                                                               join otd in this._DBERP.OrderItemDetails
                                                               on master.Id equals otd.OrderId into itemmaster
                                                               from item in itemmaster.Where(f => f.IsActive == true).DefaultIfEmpty()
                                                               where m.IsActive == true && master.SaleDate.Value.Month == DateTime.Today.Month
                                                               select new Totalmirrorandsaledetails() { mirrorid = m.Id, orderid = master.Id == null ? 0 : master.Id, itemid = item.Id == null ? 0 : item.Id, salevalue = (long)item.PriceInr == null ? (long)0.0 : (long)item.PriceInr, unitid = m.unitid, orderdate = master.SaleDate, mirrordate = m.Date, cancelstatus = item.CancelStatus ?? 0 }).ToListAsync();

            //_mirror._sale.noofarival_unit1 = _salelocal.Where(a => a.unitid == 1 && a.cancelstatus == 0).Select(a => a.mirrorid).Distinct().Count();
            //_mirror._sale.noofarival_unit2 = _salelocal.Where(a => a.unitid == 2 && a.cancelstatus == 0).Select(a => a.mirrorid).Distinct().Count();
            _mirror._sale.noofsales_unit1 = _salelocal.Where(a =>  a.cancelstatus == 0).Select(a => a.orderid).Distinct().Count(b => b != 0);
            _mirror._sale.noofsales_unit2 = _salelocal.Where(a =>  a.cancelstatus == 0 && a.orderdate.Value.Date>=DateTime.Today).Select(a => a.orderid).Distinct().Count(b => b != 0);
            _mirror._sale.totalsalevalue_unit1 = _salelocal.Where(a => a.cancelstatus == 0).GroupBy(a => a.orderid).Select(x => new { x.Key, totalval = x.Sum(y => (long?)y.salevalue) }).Sum(a => a.totalval);

            _mirror._sale.totalsalevalue_unit2 = _salelocal.Where(a => a.cancelstatus == 0 && a.orderdate.Value.Date >= DateTime.Today).GroupBy(a => a.orderid).Select(x => new { x.Key, totalval = x.Sum(y => (long?)y.salevalue) }).Sum(a => a.totalval);


            return _mirror;

        }
    }
}
