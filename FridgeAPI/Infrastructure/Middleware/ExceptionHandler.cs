using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using FridgeAPI.Domain.Contracts.Exceptions;
using System.Net;

namespace FridgeAPI.Infrastructure.Middleware
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
            catch (EntityNotFoundException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(e.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync("Something went wrong...");
            }
        }
    }
}
