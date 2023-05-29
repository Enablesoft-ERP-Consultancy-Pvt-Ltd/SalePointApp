
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

        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId,int Count);
        Task<ServiceResponse<ProductModel>> GetProductDetail(int ItemFinishId);
        Task<ServiceResponse<int>> CreateOrder(OrderModel _model);
        Task<ServiceResponse<bool>> AddPayment(OrderPaymentModel _model);
        Task<ServiceResponse<bool>> CancelOrder(CancelOrderModel _model);
    }
}
