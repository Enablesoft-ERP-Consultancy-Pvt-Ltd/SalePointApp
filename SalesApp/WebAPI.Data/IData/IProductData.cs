
using SalesApp.Models;
using SalesApp.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SalesApp.WebAPI.Data.IData
{
    public interface IProductData
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId);
    }
}
