using SALEERP.Models;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
  public interface INormalSaleRepository
    {
        public Task<NormalSaleVM> Init(long mirrorid);
        public Task<Int64> AddCashSale(NormalSaleVM _sale, int userid);


        public Task<Int64?> DeleteCashSale(int orderid, int userid);

        public Task<bool> FinishCashSale(int orderid, int userid);
        public Task<bool> AddDiscount(Int64 orderid, int userid, Decimal discount);

        public Task<NormalSaleVM> GetSales(Int64? mirrorid,NormalSaleVM _saledetail);
        public Task<bool> AddCustomerinfo(NormalSaleVM _sale, int userid);
        public Task<bool> AddStandSale(NormalSaleVM _sale, int userid);
        public  Task<List<StandSearchVM>> BindStand(string standcode);
        public Task<bool> AddDeliveryDetails(NormalSaleVM _sale, int userid);
        public Task<Int64?> DeleteStandSale(int standid, int userid);
        public Task<bool> AddSalePayment(NormalSaleVM _sale, int userid);
        public Task<Int64?> DeletePayment(int payid, int userid);
        public Task<List<StockDetailVM>> GetStock(string terms);
        public Task<Int64> AddCustomSale(NormalSaleVM _sale, int userid);
        public  Task<NormalSaleVM> Invoice(long orderid, int userid);
        public Task<string> IsExist(string stockno);
        public  Task<string> GetTeleCode(int countryid);
    }
}
