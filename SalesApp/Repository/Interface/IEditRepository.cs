using System;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SalesApp.Repository.Interface
{
   public interface IEditRepository
    {
        EditVM getAllOrders();
        EditVM getAllTempOrders();
        public Task<Int64?> Delete(long orderid, int userid);
    }
}
