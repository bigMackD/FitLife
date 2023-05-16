using System;
using System.IO;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Reports;
using FitLife.Contracts.Response.Reports;
using FitLife.Shared.Infrastructure.Helpers;
using FitLife.Shared.Infrastructure.QueryHandler;

namespace FitLife.Infrastructure.QueryHandlers.Reports
{
    public class GetReportQueryHandler : IAsyncQueryHandler<GetReportQuery, GetReportResponse>
    {
        private readonly IFileHelper _fileHelper;

        public GetReportQueryHandler(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public async Task<GetReportResponse> Handle(GetReportQuery query)
        {
            var path = Path.Combine(_fileHelper.GetRootDirectoryFullPath("Reports"), query.Id.ToString() + ".xlsx");
            var fileBytes = await File.ReadAllBytesAsync(path);
            return new GetReportResponse
            {
                ReportStream = new MemoryStream(fileBytes)
            };
        }
    }
}
