using SALEERP.Models;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalesApp.Repository.Interface
{
    public interface IStandDeliveryRepository
    {
        public Task<List<StockDetailVM>> GetStand(string terms);
        public Task<CustomSaleVM> Init();
        public Task<Int64> AddStandSale(CustomSaleVM _sale, int userid);
        public Task<CustomSaleVM> GetStandSales(long mirrorid, CustomSaleVM _saledetail);
    }
}
