using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var bytes = Encoding.UTF8.GetBytes(context.Exception.Message);
            context.HttpContext.Response.StatusCode = 400;
            context.HttpContext.Response.Body.WriteAsync(bytes,0,bytes.Length);
        }
    }
}
