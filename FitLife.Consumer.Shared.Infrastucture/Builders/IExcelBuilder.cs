using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace FitLife.Consumer.Shared.Infrastructure.Builders
{
    public interface IExcelBuilder<in T>
    {
        IExcelBuilder<T> AddWorksheet(string worksheetName);
        IExcelBuilder<T> WithContent(string worksheetName, string startingCell, IEnumerable<T> content, TableStyles style);
        ExcelPackage SaveAs(string fileName);
    }
}
