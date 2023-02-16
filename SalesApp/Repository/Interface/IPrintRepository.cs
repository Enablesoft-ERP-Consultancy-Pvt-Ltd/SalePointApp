using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
   public interface IPrintRepository
    {
        PrintVM getAllOrders();
    }
}
