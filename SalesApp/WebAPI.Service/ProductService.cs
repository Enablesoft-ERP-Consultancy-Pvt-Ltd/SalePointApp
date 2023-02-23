using SalesApp.WebAPI.Data.IData;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

using SalesApp.Models.Product;
using SalesApp.Models;
using System;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace SalesApp.WebAPI.Service
{
    public class ProductService : IProductService
    {

        private readonly IProductData data;
        public ProductService(IProductData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId)
        {
            return await data.GetProductList(StoreId);
        }

        public async Task<ServiceResponse<ProductModel>> GetProductDetail(int ItemFinishId)
        {
            return await data.GetProductDetail(ItemFinishId);
        }

        public async Task<Tuple<int, bool>> CreateOrder(OrderModel _model)
        {
            return await data.CreateOrder(_model);
        }

        public async Task<bool> AddPayment(OrderPaymentModel _model)
        {
            return await data.AddPayment(_model);
        }



    }
}
