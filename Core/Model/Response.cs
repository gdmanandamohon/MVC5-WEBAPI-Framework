using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Model
{
    public class Response<T>
    {
        public Response()
        {
            Success = true;
        }
        public bool Success { get; set; }

        public T Data { get; set; }

        public int DataCount { get; set; }

        public string ErrorMessage { get; set; }
    }
}