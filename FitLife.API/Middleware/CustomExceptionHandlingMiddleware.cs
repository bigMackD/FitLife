using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FitLife.API.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions
    /// </summary>
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">Request delegate</param>
        public CustomExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes error handling on <see cref="HttpContext"/>
        /// </summary>
        /// <param name="context"></param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(context, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
                var errorId = Guid.NewGuid();
                Log.ForContext("ErrorId", errorId)
                    .Error(exception, "Error occured in API");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsJsonAsync(new
                {
                    ErrorId = errorId,
                    Message = "Something bad happened in API. " +
                              "Contact admin if the issue persists."
                });
            }
        }
}
