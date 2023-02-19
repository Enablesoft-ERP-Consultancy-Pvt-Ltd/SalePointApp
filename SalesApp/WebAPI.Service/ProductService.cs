using SalesApp.WebAPI.Data.IData;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

using SalesApp.Models.Product;
using SalesApp.Models;

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




    }
}
