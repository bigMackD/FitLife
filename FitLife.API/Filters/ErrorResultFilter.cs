using System.Linq;
using System.Net;
using FitLife.Shared.Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FitLife.API.Filters
{
    /// <summary>
    /// Result filter to check for business logic errors
    /// </summary>
    public class ErrorResultFilter : IResultFilter
    {
        /// <summary>
        /// Checks result for errors and sets 409 http status code
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var responseResult = context.Result as ObjectResult;
            if (responseResult?.Value is IBaseResponse baseResponse && baseResponse?.Errors != null && baseResponse?.Errors?.Any() == true)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }

        /// <summary>
        /// Checks result after execution, currently not in use
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Nothing to do here
        }
    }

}
