using SalesApp.Model.Account;
using SalesApp.Model.User;
using SalesApp.WebAPI.Data.IData;
using SalesApp.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesApp.Model;

namespace SalesApp.WebAPI.Service
{
    public class AccountService : IAccountService
    {

        private readonly IAccountData data;
        public AccountService(IAccountData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<UserModel>> LogInUser(LoginModel model)
        {
            return await data.LogInUser(model);
        }

        public async Task<ServiceResponse<string>> LogOutUser(int userId)
        {

            return await data.LogOutUser(userId);
        }

        public async Task<ServiceResponse<string>> AddUser(AccountUserModel _model)
        {
            return await data.AddUser(_model);
        }

        public async Task<ServiceResponse<IEnumerable<AccountUserModel>>> GetUser(int userType)
        {
            return await data.GetUser(userType);
        }

        public async Task<ServiceResponse<UserItem>> GetUserDetail(int UserId)
        {
            return await data.GetUserDetail(UserId);
        }



    }
}
