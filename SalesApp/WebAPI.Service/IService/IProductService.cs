
using System.Collections.Generic;
using System.Threading.Tasks;

using SalesApp.Models.Product;
using SalesApp.Models;

namespace SalesApp.WebAPI.Service.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId);
    }
}
