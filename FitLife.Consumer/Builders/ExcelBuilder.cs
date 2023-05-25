using FitLife.Consumer.Shared.Infrastructure.Builders;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace FitLife.Consumer.Builders
{
    public sealed class ExcelBuilder<T> : IExcelBuilder<T>
    {
        private readonly ExcelPackage _excelPackage;
        public ExcelBuilder()
        {
            _excelPackage = new ExcelPackage();
        }

        public IExcelBuilder<T> AddWorksheet(string worksheetName)
        {
            var workbook = _excelPackage.Workbook.Worksheets.FirstOrDefault(worksheet => worksheet.Name == worksheetName);
            if (workbook == null)
            { 
                _excelPackage.Workbook.Worksheets.Add(worksheetName);
            }
            return this;
        }

        public IExcelBuilder<T> WithContent(string worksheetName, string startingCell, IEnumerable<T> content, TableStyles style)
        {
            var worksheet = _excelPackage.Workbook.Worksheets.FirstOrDefault(worksheet => worksheet.Name == worksheetName);
            if (worksheet != null)
            {
                worksheet.Cells[startingCell].LoadFromCollection(content, true, style);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[worksheet.Dimension.Address].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[worksheet.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            return this;
        }

        public ExcelPackage SaveAs(string fileName)
        {
            _excelPackage.SaveAs(new FileInfo(fileName+ ".xlsx"));
            _excelPackage.Dispose();
            return _excelPackage;
        }

    }
}
