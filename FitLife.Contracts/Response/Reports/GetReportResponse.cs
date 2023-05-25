using System.IO;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Reports
{
    /// <summary>
    /// Reponse for report file
    /// </summary>
    public sealed class GetReportResponse : IBaseResponse
    {
        /// <summary>
        /// Stream with report file
        /// </summary>
        public Stream ReportStream { get; set; }

        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }
    }
}
