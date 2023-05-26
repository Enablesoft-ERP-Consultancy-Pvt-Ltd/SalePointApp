using SalesApp.WebAPI.Data.IData;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

using SalesApp.Models.Product;
using SalesApp.Models;
using System;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Linq;

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
            ServiceResponse<ProductModel> result = await data.GetProductDetail(ItemFinishId);
            var model = result.Data;

            if(model.StockList.Count>0)
            {
                model.Quantity = model.StockList.Count;
                model.Price = model.StockList.Average(x => x.Price);
            }
            else
            {

                model.Quantity = 0;
            }


    
            result.Data = model;
            return result;
        }

        public async Task<ServiceResponse<int>> CreateOrder(OrderModel _model)
        {
            return await data.CreateOrder(_model);
        }

        public async Task<ServiceResponse<bool>> AddPayment(OrderPaymentModel _model)
        {
            return await data.AddPayment(_model);
        }

        public async Task<ServiceResponse<bool>> CancelOrder(CancelOrderModel _model)
        {
            return await data.CancelOrder(_model);
        }

    }
}
