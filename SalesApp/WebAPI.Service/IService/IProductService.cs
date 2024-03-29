﻿
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
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId, int Count);
        Task<ServiceResponse<ProductModel>> GetProductDetail(int ItemFinishId);
        Task<ServiceResponse<int>> CreateOrder(OrderModel _model);
        Task<ServiceResponse<bool>> AddPayment(OrderPaymentModel _model);
        Task<ServiceResponse<bool>> CancelOrder(CancelOrderModel _model);

        Task<ServiceResponse<bool>> AddWishItem(WishItemModel _model);
        Task<ServiceResponse<bool>> DelWishItem(int WishId);
        Task<ServiceResponse<IEnumerable<WishModel>>> GetWishList(int StoreId, int CustomerId);

        Task<ServiceResponse<IEnumerable<BillModel>>> GetAllWebOrder();
        Task<ServiceResponse<dynamic>> GetOrderDeatil(long orderId);

        Task<ServiceResponse<IEnumerable<dynamic>>> GetFilter(int StoreId);

    }
}
