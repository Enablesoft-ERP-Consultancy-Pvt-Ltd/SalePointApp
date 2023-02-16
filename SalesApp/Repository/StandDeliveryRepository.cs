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

namespace SalesApp.Repository
{
    public class StandDeliveryRepository:IStandDeliveryRepository
    {
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        public StandDeliveryRepository(Sales_ERPContext salesdbcontext, ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }

        public async Task<long> AddStandSale(CustomSaleVM _sale, int userid)
        {

            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid && i.MirrorId == 2).ConfigureAwait(false);
                    if (entityorder != null && entityorder.Id > 0)
                    {
                        uid = entityorder.Id;
                    }
                    else
                    {
                        await this._SALESDBE.OrderMaster.AddAsync(new OrderMaster()
                        {
                            MirrorId = 2,
                            SaleDate = DateTime.Now,
                            Unit = 1,
                            Description = "StandSale",
                            TransactionId = Common.GetUnique(),
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            salestatus = 1

                        }).ConfigureAwait(false);

                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                    if (uid > 0)
                    {

                        await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                        {
                            OrderId = uid,
                            ItemDesc = _sale.item_desc,
                            StockId = _sale.stockno,
                            OrderType = (int?)SaleType.OF,
                            OrderTypePrefix = _sale.saletypevalue,
                            ItemType = 2,
                            Price = _sale.totalvalue,
                            PriceInr = _sale.totalvalue,
                            ConversionRate = _sale.conversionrate,
                            Unit = 1,
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            SaleType = 3

                        }).ConfigureAwait(false);
                        //result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        // long oid = await this._SALESDBE.OrderItemDetails.MaxAsync(p => p.Id).ConfigureAwait(false);
                        //foreach (var item in _sale.specialadditions)
                        //{
                        //    if (item.Selected)
                        //    {
                        //        if (item != null)
                        //        {
                        //            await this._SALESDBE.SpecialAdditionDetails.AddAsync(new SpecialAdditionDetails() { OrderItemId = oid, SpecialAdditionDesc = item.Text, SpecialAdditionId = Convert.ToInt32(item.Value), CreatedBy = userid, CreatedDatetime = DateTime.Now, IsActive = true }).ConfigureAwait(false);
                        //        }
                        //    }
                        //}
                        innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);

                        }
                        else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }


                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return uid;

        }

        public async Task<List<StockDetailVM>> GetStand(string stockid)
        {
            List<StockDetailVM> _list;
            try
            {
                _list = await (from c in this._ERPDB.CarpetNumber
                               join vf in this._ERPDB.V_FinishedItemDetail
                               on c.item_finished_id equals vf.ITEM_FINISHED_ID
                               where c.Prefix == stockid
                               select new StockDetailVM
                               {
                                   stockid = c.TStockNo,
                                   category = vf.CATEGORY_NAME,
                                   itemname = vf.ITEM_NAME,
                                   marblecolor = vf.COLORNAME,
                                   price = c.Price,
                                   size = vf.SizeInch,
                                   marblestone = vf.QUALITYNAME


                               }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //StockDetailVM _sdetails = new StockDetailVM();

            return _list;


        }

        public async Task<CustomSaleVM> Init()
        {
            CustomSaleVM _cashsaledetails = new CustomSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.shapesdetails = await _comm.GetShapes();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.marblecolordetails = await _comm.GetMarbleColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
            _cashsaledetails.countrydetails = await _comm.GetCountries();
            _cashsaledetails.sizeinwidth = await _comm.GetWidth();
            _cashsaledetails.sizeinheight = await _comm.GetLenght();
            _cashsaledetails.standcategory = await _comm.GetItemName();

            return _cashsaledetails;
        }
        public async Task<CustomSaleVM> GetStandSales(long _orderid, CustomSaleVM _cashsaledetails)
        {
            //  CustomSaleVM _cashsaledetails = new CustomSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.cashsaledetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId

                                                      where m.Id == _orderid 
                                                      select new customsaledetails
                                                      {
                                                          itemorderid = od.Id,
                                                          stockid = od.StockId,
                                                          //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                          itemdesc = od.ItemDesc,
                                                          ordertype = od.OrderTypePrefix,
                                                          salevalue = od.Price,
                                                          salevalueinr = od.PriceInr

                                                      }).ToListAsync();

            _cashsaledetails.standsaledetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId

                                                      where m.Id == _orderid && od.ItemType == 2
                                                      select new standdetails
                                                      {
                                                          itemorderid = od.Id,
                                                          color = od.Color,
                                                          //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                          standdesc = od.ItemDesc,
                                                          ordertype = od.OrderTypePrefix,
                                                          width = "1",
                                                          height = "2"

                                                      }).ToListAsync();
            //_cashsaledetails.currencydetails = await _comm.GetCurrency();
            //_cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);

            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.shapesdetails = await _comm.GetShapes();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.marblecolordetails = await _comm.GetMarbleColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
            _cashsaledetails.countrydetails = await _comm.GetCountries();
            _cashsaledetails.sizeinwidth = await _comm.GetWidth();
            _cashsaledetails.sizeinheight = await _comm.GetLenght();
            _cashsaledetails.standcategory = await _comm.GetItemName();
            _cashsaledetails.orderid = _orderid;
            return _cashsaledetails;

        }
    }
}
