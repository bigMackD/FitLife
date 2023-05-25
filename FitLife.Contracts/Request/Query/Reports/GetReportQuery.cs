using System;
using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Reports
{
    /// <summary>
    /// Query for retrieving report
    /// </summary>
    public sealed class GetReportQuery : IQuery
    {
        /// <summary>
        /// Id of an report
        /// </summary>
        public Guid Id { get; set; }
    }
}
