using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Meals
{
    /// <summary>
    /// Response for meal details
    /// </summary>
    public sealed class GetMealDetailsResponse : IBaseResponse
    {
        /// <summary>
        /// Meal details
        /// </summary>
        public MealResponse Meal { get; set; }

        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }
    }

    /// <summary>
    /// Meal details
    /// </summary>
    public class MealResponse
    {
        /// <summary>
        /// Id of a meal
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of a meal
        /// </summary>
        /// <example> Pizza </example>
        public string Name { get; set; }

        /// <summary>
        /// Calories of a meal
        /// </summary>
        public decimal Calories { get; set; }

        /// <summary>
        /// Grams of proteins
        /// </summary>
        public decimal ProteinsGrams { get; set; }

        /// <summary>
        /// Grams of carbohydrates 
        /// </summary>
        public decimal CarbsGrams { get; set; }

        /// <summary>
        /// Grams of fats
        /// </summary>
        public decimal FatsGrams { get; set; }

        /// <summary>
        /// List of a products for a meal
        /// </summary>
        public ICollection<MealProductDetails> MealProducts { get; set; }

        /// <summary>
        /// Category of a meal
        /// </summary>
        public CategoryResponse Category { get; set; }
    }

    /// <summary>
    /// Response of a category
    /// </summary>
    public class CategoryResponse
    {
        /// <summary>
        /// Id of a category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of a category
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Details of a product of a meal
    /// </summary>
    public class MealProductDetails
    {
        /// <summary>
        /// Name of a product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of a product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Grams of a product
        /// </summary>
        public int Grams { get; set; }

        /// <summary>
        /// Calories of product
        /// </summary>
        public decimal Calories { get; set; }

        /// <summary>
        /// Grams of proteins
        /// </summary>
        public decimal ProteinsGrams { get; set; }

        /// <summary>
        /// Grams of carbohydrates 
        /// </summary>
        public decimal CarbsGrams { get; set; }

        /// <summary>
        /// Grams of fats
        /// </summary>
        public decimal FatsGrams { get; set; }
    }
}
