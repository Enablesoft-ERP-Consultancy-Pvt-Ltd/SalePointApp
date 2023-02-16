using SALEERP.Models;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
   public interface ICashSaleRepository
    {
        public Task<List<StockDetailVM>> GetStock(string terms);
        public Task<CashSaleVM> Init(long mirrorId);
        public Task<Int64> AddCashSale(CashSaleVM _user, int userid);

        public Task<bool> DeleteCashSale(int orderid,int userid);

        public Task<bool> FinishCashSale(int orderid, int userid);

        public Task<CashSaleVM> GetSales(long mirrorid, CashSaleVM _sale);
        public Task<long> GetOderIdByOrderItemId(int orderItemId);
        public Task<bool> AddOrderPayment(int orderItemId, int userid);
    }
}
