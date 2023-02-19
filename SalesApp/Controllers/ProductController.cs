using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Models.Product;
using SalesApp.Models;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

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


  






    }
}
