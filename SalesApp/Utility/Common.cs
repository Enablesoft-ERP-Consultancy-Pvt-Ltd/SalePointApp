using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Utility
{
    public static class Common
    {
        public static  string GetUnique()
        {
            var id = new Guid();
            return id.ToString();
        }
    }
}
