using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.MealCategories
{
    public class GetMealCategoriesResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public IEnumerable<MealCategory> MealCategories { get; set; }
    }

    public  class MealCategory
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
    }
}
