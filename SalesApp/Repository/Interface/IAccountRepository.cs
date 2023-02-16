using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
  public  interface IAccountRepository
    {
      //  bool ManageUser(UserLoginVM _user, out int uid);
        bool ManageUser(UserLoginVM _user, out int uid,out string uname,out string profilepicture,out int roletype);
    }
}
