using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Service
{
    public class BaseService
    {
        protected Response<T> SafeExecute<T>(Func<T> exec)
        {
            var response = new Response<T>();
            try
            {
                response.Data = exec();
                response.Success = true;
            }
            catch (SystemException exp)
            {
                response.ErrorMessage = exp.Message;
                response.Success = false;
                //throw;
            }
            return response;
        }
    }
}