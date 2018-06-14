using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebsite.Filters {
    public class ExceptionFilter : Attribute, IExceptionFilter {
        public void OnException (ExceptionContext context) {
            context.HttpContext.Response.WriteAsync ($"{GetType().Name} in. \r\n");
        }
    }

    public class AsyncExceptionFilter : Attribute, IAsyncExceptionFilter {
        public Task OnExceptionAsync (ExceptionContext context) {
            context.HttpContext.Response
                .WriteAsync ($"{GetType().Name} catch exception. Message: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}