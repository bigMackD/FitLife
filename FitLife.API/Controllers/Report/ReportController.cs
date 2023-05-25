using System;
using System.IO;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Reports;
using FitLife.Contracts.Response.Reports;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Report
{
    /// <summary>
    /// Controller for managing reports
    /// </summary>
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IAsyncQueryHandler<GetReportQuery, GetReportResponse> _getReportQueryHandler;

        /// <summary>
        /// Controller for downloading reports
        /// </summary>
        /// <param name="getReportQueryHandler"></param>
        public ReportController(IAsyncQueryHandler<GetReportQuery, GetReportResponse> getReportQueryHandler)
        {
            _getReportQueryHandler = getReportQueryHandler;
        }

        /// <summary>
        /// Gets report by specified ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Report file</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Authorize]
        [Route("calories/{id}")]
        [ProducesResponseType(typeof(File), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var query = new GetReportQuery { Id = id };
            var response = await _getReportQueryHandler.Handle(query);
            var fileName = $"MyCaloriesReport-{DateTime.Now.Date:dd-MM-yy}.xlsx";
            return File(response.ReportStream, "application/octet-stream", fileName);
        }
    }
}
