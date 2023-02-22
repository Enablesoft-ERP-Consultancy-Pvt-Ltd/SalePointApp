
using SalesApp.Models;
using SalesApp.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SalesApp.WebAPI.Data.IData
{
    public interface IProductData
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId);
        Task<Tuple<int, bool>> CreateOrder(OrderModel _model);
        Task<bool> AddPayment(OrderPaymentModel _model);
    }
}
