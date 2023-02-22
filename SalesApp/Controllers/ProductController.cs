﻿using DocumentFormat.OpenXml.Drawing.Charts;
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

        [HttpPost("createOrder")]
        [ProducesResponseType(typeof(ServiceResponse<Tuple<int, bool>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<Tuple<int, bool>>), StatusCodes.Status400BadRequest)]
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
                model.Description = "NormalSale";
                model.SessionYear = DateTime.Now.Year;
                model.SaleStatus = (short)SalesStatus.Ordered;
                model.IsActive = true;

                model.Unit = 1;
                model.DisCountPer = 10;
                model.MirrorId = -1;


                foreach (var item in model.ItemList)
                {
                    item.PriceINR = item.Price;
                    item.Qty = 1;
                    item.Unit = 1;
                    item.CurrencyType = 6;
                    item.SalesType = (short)SaleType.OF;
                    item.ItemType = 1;
                    item.OrderType = 1;
                    item.OrderTypePrefix = SaleType.OF.ToString();
                    item.ConversionRate = 1;
                    item.SessionYear = DateTime.Now.Year;
                    item.CreatedBy = 1;
                    item.CreatedOn = DateTime.Now;
                    item.IsActive = true;
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
                model.CreatedBy = 1;
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


