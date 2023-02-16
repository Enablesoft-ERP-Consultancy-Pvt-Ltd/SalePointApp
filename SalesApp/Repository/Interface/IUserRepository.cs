using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface IUserRepository
    {
        List<UserLoginVM> getAllUsers();
        bool AddUser(UserLoginVM _user, int userid);
        bool UpdateUser(UserLoginVM _user, int uid);
        bool DeleteUser(int uid);
        UserLoginVM EditUser(int uid);
    }
}
