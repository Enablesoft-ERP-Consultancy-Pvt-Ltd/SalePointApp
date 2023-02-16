
using SalesApp.Models;
using SalesApp.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SalesApp.WebAPI.Data.IData
{

    public interface IProductData
    {



        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId);


        //Task<ServiceResponse<UserModel>> LogInUser(LoginModel model);
        //Task<ServiceResponse<string>> LogOutUser(int userId);
        //Task<ServiceResponse<string>> AddUser(AccountUserModel _model);
        //Task<ServiceResponse<IEnumerable<AccountUserModel>>> GetUser(int userType);
        //Task<ServiceResponse<UserItem>> GetUserDetail(int UserId);
    }
}
