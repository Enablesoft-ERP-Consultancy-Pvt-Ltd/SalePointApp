
using System.Collections.Generic;
using System.Threading.Tasks;

using SalesApp.Models.Product;
using SalesApp.Models;
using System;

namespace SalesApp.WebAPI.Service.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId);
        Task<Tuple<int, bool>> CreateOrder(OrderModel _model);
        Task<bool> AddPayment(OrderPaymentModel _model);
    }
}
