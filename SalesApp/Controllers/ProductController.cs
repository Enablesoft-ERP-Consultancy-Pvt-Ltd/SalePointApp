
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Models.Product;
using SalesApp.Models;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SalesApp.Utility;


namespace SalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService prodSrv;
        public ProductController(IProductService _prodSrv)
        {
            prodSrv = _prodSrv;
        }

        [HttpGet("getProductList/{StoreId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ProductModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ProductModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductList(int StoreId)
        {
            return Ok(await prodSrv.GetProductList(StoreId));
        }


        [HttpGet("getProductList/{StoreId}/{Count}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ProductModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ProductModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductList(int StoreId, int Count)
        {
            return Ok(await prodSrv.GetProductList(StoreId, Count));
        }

        [HttpGet("getProductListDetail/{ProductId}")]
        [ProducesResponseType(typeof(ServiceResponse<ProductModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<ProductModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductDetail(int ProductId)
        {
            return Ok(await prodSrv.GetProductDetail(ProductId));
        }


        [HttpPost("createOrder")]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel model)
        {
            try
            {
                model.CreatedBy = 1;
                model.CreatedOn = DateTime.Now;
                model.SaleDate = DateTime.Now;
                model.DelieveryType = 0;
                model.PortType = 0;
                model.TransactionId = Common.GetUnique();
                model.Description = "WebSales";
                model.SessionYear = DateTime.Now.Year;
                model.SaleStatus = (short)SalesStatus.Ordered;
                model.IsActive = true;
                model.Unit = 1;
                model.DisCountPer = 10;

                foreach (var item in model.ItemList)
                {
                    item.TransId = model.TransactionId;
                    item.PackId = 101;
                    item.PriceINR = item.Price;
                    item.Unit = 1;
                    item.CurrencyType = 6;
                    item.SalesType = (short)SaleType.OF;
                    item.ItemType = 1;
                    item.OrderType = 1;
                    item.OrderTypePrefix = SaleType.OF.ToString();
                    item.ConversionRate = 1;
                    item.SessionYear = DateTime.Now.Year;
                    item.CreatedOn = DateTime.Now;
                    item.IsActive = true;
                    item.Source = "WebSales";
                    item.CreatedBy = 1;
                }
                return Ok(await prodSrv.CreateOrder(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("addPayment")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddPayment([FromBody] OrderPaymentModel model)
        {
            try
            {
                model.CreatedBy = 1;
                model.PaymentMode = (short)PaymentMode.BankTransfer;
                model.CardType = (short)CardType.Other;
                model.Currency = 6;
                model.PaylaterStatus = 0;
                model.PaymentDate = DateTime.Now;
                model.IsActive = true;
                model.CreatedOn = DateTime.Now;


                return Ok(await prodSrv.AddPayment(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost("cancelOrder")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderModel model)
        {
            try
            {
                model.CreatedBy = 1;
                model.PackingDetailId = 0;
                model.BillId = 0;
                model.IsActive = false;
                model.CreatedOn = DateTime.Now;
                return Ok(await prodSrv.CancelOrder(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost("addWishItem")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddWishItem([FromBody] WishItemModel model)
        {
            try
            {

                model.IsActive = 1;
                model.IsPublished = true;
                model.CreatedOn = DateTime.Now;
                return Ok(await prodSrv.AddWishItem(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpDelete("delWishItem/{WishId}/")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelWishItem(int WishId)
        {
            try
            {
                return Ok(await prodSrv.DelWishItem(WishId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpGet("getWishList/{StoreId}/{CustomerId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<WishModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<WishModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWishList(int StoreId, int CustomerId)
        {
            return Ok(await prodSrv.GetWishList(StoreId, CustomerId));
        }













    }
}


