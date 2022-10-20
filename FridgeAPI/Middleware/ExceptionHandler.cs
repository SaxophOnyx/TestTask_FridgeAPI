using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FridgeAPI.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;


        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync("Something went wrong...");
        }
    }
}
