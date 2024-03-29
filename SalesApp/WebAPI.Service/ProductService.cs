﻿using SalesApp.WebAPI.Data.IData;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

using SalesApp.Models.Product;
using SalesApp.Models;
using System;

using System.Linq;
using System.Collections;
using SalesApp.Utility;



namespace SalesApp.WebAPI.Service
{
    public class ProductService : IProductService
    {

        private readonly IProductData data;
        public ProductService(IProductData ldata)
        {
            data = ldata;
        }


        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId, int Count)
        {
            var result = await data.GetProductList(StoreId, Count);
            var modelList = result.Data;
            List<ProductModel> productModels = new List<ProductModel>();
            foreach (var model in modelList)
            {
                var items = model.ItemList;
                if (items.Count > 0)
                {
                    model.CollectionName = (items.Where(x => x.CategoryId == (int)AttributeList.Collection).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Collection).FirstOrDefault().ItemName : string.Empty);
                    model.ConstructionName = (items.Where(x => x.CategoryId == (int)AttributeList.Construction).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Construction).FirstOrDefault().ItemName : string.Empty);
                    model.MaterialName = (items.Where(x => x.CategoryId == (int)AttributeList.Material).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Material).FirstOrDefault().ItemName : string.Empty);
                    model.OriginName = (items.Where(x => x.CategoryId == (int)AttributeList.Origin).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Origin).FirstOrDefault().ItemName : string.Empty);
                    model.TextureName = (items.Where(x => x.CategoryId == (int)AttributeList.Texture).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Texture).FirstOrDefault().ItemName : string.Empty);
                    model.StyleName = (items.Where(x => x.CategoryId == (int)AttributeList.Style).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Style).FirstOrDefault().ItemName : string.Empty);
                    model.OtherInformation = (items.Where(x => x.CategoryId == (int)AttributeList.OtherInfo).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.OtherInfo).FirstOrDefault().ItemName : string.Empty);
                }
                productModels.Add(model);
            }

            result.Data = productModels;
            return result;
        }


        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId)
        {
            var result = await data.GetProductList(StoreId);
            var modelList = result.Data;
            List<ProductModel> productModels = new List<ProductModel>();
            foreach (var model in modelList)
            {
                var items = model.ItemList;
                if (items.Count > 0)
                {
                    model.CollectionName = (items.Where(x => x.CategoryId == (int)AttributeList.Collection).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Collection).FirstOrDefault().ItemName : string.Empty);
                    model.ConstructionName = (items.Where(x => x.CategoryId == (int)AttributeList.Construction).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Construction).FirstOrDefault().ItemName : string.Empty);
                    model.MaterialName = (items.Where(x => x.CategoryId == (int)AttributeList.Material).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Material).FirstOrDefault().ItemName : string.Empty);
                    model.OriginName = (items.Where(x => x.CategoryId == (int)AttributeList.Origin).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Origin).FirstOrDefault().ItemName : string.Empty);
                    model.TextureName = (items.Where(x => x.CategoryId == (int)AttributeList.Texture).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Texture).FirstOrDefault().ItemName : string.Empty);
                    model.StyleName = (items.Where(x => x.CategoryId == (int)AttributeList.Style).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Style).FirstOrDefault().ItemName : string.Empty);
                    model.OtherInformation = (items.Where(x => x.CategoryId == (int)AttributeList.OtherInfo).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.OtherInfo).FirstOrDefault().ItemName : string.Empty);
                }
                productModels.Add(model);
            }

            result.Data = productModels;
            return result;
        }

        public async Task<ServiceResponse<ProductModel>> GetProductDetail(int ItemFinishId)
        {
            ServiceResponse<ProductModel> result = await data.GetProductDetail(ItemFinishId);
            var model = result.Data;
            var items = model.ItemList;
            if (items.Count > 0)
            {
                model.CollectionName = (items.Where(x => x.CategoryId == (int)AttributeList.Collection).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Collection).FirstOrDefault().ItemName : string.Empty);

                model.ConstructionName = (items.Where(x => x.CategoryId == (int)AttributeList.Construction).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Construction).FirstOrDefault().ItemName : string.Empty);

                model.MaterialName = (items.Where(x => x.CategoryId == (int)AttributeList.Material).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Material).FirstOrDefault().ItemName : string.Empty);

                model.OriginName = (items.Where(x => x.CategoryId == (int)AttributeList.Origin).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Origin).FirstOrDefault().ItemName : string.Empty);

                model.TextureName = (items.Where(x => x.CategoryId == (int)AttributeList.Texture).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Texture).FirstOrDefault().ItemName : string.Empty);

                model.StyleName = (items.Where(x => x.CategoryId == (int)AttributeList.Style).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.Style).FirstOrDefault().ItemName : string.Empty);

                model.OtherInformation = (items.Where(x => x.CategoryId == (int)AttributeList.OtherInfo).Count() > 0 ? items.Where(x => x.CategoryId == (int)AttributeList.OtherInfo).FirstOrDefault().ItemName : string.Empty);

            }
            if (model.StockList.Count > 0)
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



        public async Task<ServiceResponse<bool>> AddWishItem(WishItemModel _model)
        {
            return await data.AddWishItem(_model);
        }

        public async Task<ServiceResponse<bool>> DelWishItem(int WishId)
        {
            return await data.DelWishItem(WishId);
        }

        public async Task<ServiceResponse<IEnumerable<WishModel>>> GetWishList(int StoreId, int CustomerId)
        {
            return await data.GetWishList(StoreId, CustomerId);
        }



        public async Task<ServiceResponse<IEnumerable<BillModel>>> GetAllWebOrder()
        {
            return await data.GetAllWebOrder();
        }



        public async Task<ServiceResponse<dynamic>> GetOrderDeatil(long orderId)
        {
            return await data.GetOrderDeatil(orderId);
        }

        public async Task<ServiceResponse<IEnumerable<dynamic>>> GetFilter(int StoreId)
        {
            return await data.GetFilter(StoreId);
        }


    }
}
