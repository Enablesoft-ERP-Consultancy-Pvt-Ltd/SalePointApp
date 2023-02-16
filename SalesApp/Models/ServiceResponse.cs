using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Result { get; set; } = false;

        public string Message { get; set; } = null;
    }
}
