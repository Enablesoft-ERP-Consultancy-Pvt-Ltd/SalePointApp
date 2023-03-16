using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Models.Product;
using SalesApp.Models;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Aspose.BarCode.Generation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using SalesApp.Utility;
using DocumentFormat.OpenXml.Vml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Excel;
using SALEERP.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IActionResult> CancelOrder([FromBody] OrderPaymentModel model)
        {
            try
            {


//                Select* from sales.Order_Master x Inner Join
//sales.Order_Item_Details y on x.id = y.order_id Where y.order_id = 21
//Update y SET y.is_active = 0,y.updated_by = y.id,y.updated_datetime = y.order_id ,y.bill_id = 0
//from sales.Order_Master x Inner Join
//sales.Order_Item_Details y on x.id = y.order_id Where y.order_id = 21
//Select* from sales.Order_Master x Inner Join
//sales.Order_Payment y on x.id = y.order_id Where y.order_id = 21
//Update y SET y.is_active = 0,y.updated_by = y.id
//from sales.Order_Master x Inner Join
//sales.Order_Payment y on x.id = y.order_id Where y.order_id = 21
//* ********************************
//Update x SET x.PackingDetailId = 0,x.packsource = '',x.Pack = 0,x.Pack_Date = null
//from[dbo].[CarpetNumber] x inner join[sales].Order_Item_Details y on x.TStockNo = y.stock_id
//where y.order_id = 21










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





    }
}


