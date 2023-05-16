using FitLife.Consumer.Models.Excel;
using FitLife.Consumer.Shared.Infrastructure.Builders;
using FitLife.Consumer.Shared.Infrastructure.Services.File;
using FitLife.Consumer.Shared.Infrastructure.Services.Report;
using FitLife.DB.Context;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace FitLife.Consumer.Services.Report
{
    public class ExcelReportGenerator : IExcelReportGenerator
    {
        private readonly FoodContext _context;
        private readonly IExcelBuilder<DailyIntake> _excelBuilder;
        private readonly IFileHelper _fileHelper;

        public ExcelReportGenerator(FoodContext context, 
            IExcelBuilder<DailyIntake> excelBuilder,
            IFileHelper fileHelper)
        {
            _context = context;
            _excelBuilder = excelBuilder;
            _fileHelper = fileHelper;
        }

        public ExcelPackage Generate(string userId, Guid fileId)
        {
            var userIntake = _context.UserMeals
                .Where(userMeal => userMeal.UserId == userId && userMeal.ConsumedDate.Date <= DateTime.Today.Date)
                .Include(userMeal => userMeal.Meal)
                .Include(userMeal => userMeal.Meal.MealProducts)
                .GroupBy(userMeal => userMeal.ConsumedDate.Date)
                .OrderBy(userMeal => userMeal.Key)
                .Select(userMealByDate => 
                    new DailyIntake
                {
                    Date = DateOnly.FromDateTime(userMealByDate.Key.Date),
                    CarbsGrams = userMealByDate.SelectMany(userMeal => userMeal.Meal.MealProducts)
                        .Sum(mealProduct => mealProduct.Product.CarbsGrams),
                    FatsGrams = userMealByDate.SelectMany(userMeal => userMeal.Meal.MealProducts)
                        .Sum(mealProduct => mealProduct.Product.FatsGrams),
                    ProteinsGrams = userMealByDate.SelectMany(userMeal => userMeal.Meal.MealProducts)
                        .Sum(mealProduct => mealProduct.Product.ProteinsGrams),
                    Calories = userMealByDate.SelectMany(userMeal => userMeal.Meal.MealProducts)
                        .Sum(mealProduct => mealProduct.Product.ProteinsGrams * 4 + mealProduct.Product.FatsGrams * 9 + mealProduct.Product.CarbsGrams * 4)
                });

            const string worksheetName = "Calories Raport";
            const string downloadDirectory = "Reports";
            _fileHelper.CreateAtRootDirectory(downloadDirectory);
            var fullPath = _fileHelper.GetRootDirectoryFullPath(downloadDirectory);

            var fileName = fileId.ToString();
            var uploadLocation = Path.Combine(fullPath, fileName);

            return _excelBuilder
                .AddWorksheet(worksheetName)
                .WithContent(worksheetName, "A1", userIntake, TableStyles.Light13)
                .SaveAs(uploadLocation);
        }
    }
}
