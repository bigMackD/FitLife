using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Meals
{
    /// <summary>
    /// Response of retrieving a meal
    /// </summary>
    public sealed class GetMealsResponse : IBaseResponse, IPagingResponse
    {
        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }

        /// <summary>
        /// Count of all meals
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// List of meals
        /// </summary>
        public IEnumerable<Meal> Meals { get; set; }

    }

    /// <summary>
    /// Meal response
    /// </summary>
    public class Meal
    {
        /// <summary>
        /// Id of a meal
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of a meal
        /// </summary>
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
        /// Indicates whether meal is deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}
