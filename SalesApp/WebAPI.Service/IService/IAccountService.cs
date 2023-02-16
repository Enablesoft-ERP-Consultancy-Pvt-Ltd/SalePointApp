using SalesApp.Model.Account;
using SalesApp.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesApp.Model;

namespace SalesApp.WebAPI.Service.IService
{
    public interface IAccountService
    {
        Task<ServiceResponse<UserModel>> LogInUser(LoginModel model);
        Task<ServiceResponse<string>> LogOutUser(int userId);
        Task<ServiceResponse<string>> AddUser(AccountUserModel _model);
        Task<ServiceResponse<IEnumerable<AccountUserModel>>> GetUser(int userType);
        Task<ServiceResponse<UserItem>> GetUserDetail(int UserId);
    }
}
