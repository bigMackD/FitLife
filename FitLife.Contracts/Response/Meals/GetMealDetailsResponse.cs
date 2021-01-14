using System;
using System.Collections.Generic;
using System.Text;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Meals
{
    public class GetMealDetailsResponse : IBaseResponse
    {
        public MealResponse Meal { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }

    public class MealResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal ProteinsGrams { get; set; }
        public decimal CarbsGrams { get; set; }
        public decimal FatsGrams { get; set; }
        public ICollection<MealProductDetails> MealProducts { get; set; }
        public CategoryResponse Category { get; set; }
    }

    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MealProductDetails
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Grams { get; set; }
        public decimal Calories { get; set; }
        public decimal ProteinsGrams { get; set; }
        public decimal CarbsGrams { get; set; }
        public decimal FatsGrams { get; set; }
    }
}
