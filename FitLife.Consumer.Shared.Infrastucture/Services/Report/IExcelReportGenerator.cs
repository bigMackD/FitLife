using OfficeOpenXml;

namespace FitLife.Consumer.Shared.Infrastructure.Services.Report
{
    public interface IExcelReportGenerator
    {
        ExcelPackage Generate(string userId, Guid fileId);
    }
}
