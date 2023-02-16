using SALEERP.Models;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
  public interface ICustomSaleRepository
    {
        public Task<CustomSaleVM> Init(long mirrorid);
        public Task<Int64> AddCashSale(CustomSaleVM _sale, int userid);


        public Task<Int64?> DeleteCashSale(int orderid, int userid);

        public Task<bool> FinishCashSale(int orderid, int userid);

        public Task<CustomSaleVM> GetSales(long mirrorid,CustomSaleVM _saledetail);
        public Task<bool> AddCustomerinfo(CustomSaleVM _sale, int userid);
        public Task<bool> AddStandSale(CustomSaleVM _sale, int userid);
        public Task<bool> AddDeliveryDetails(CustomSaleVM _sale, int userid);
        public Task<Int64?> DeleteStandSale(int standid, int userid);
        public Task<bool> AddSalePayment(CustomSaleVM _sale, int userid);
        public Task<Int64?> DeletePayment(int payid, int userid);
    }
}
