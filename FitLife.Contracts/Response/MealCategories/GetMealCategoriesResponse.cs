using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.MealCategories
{
    /// <summary>
    /// Response for categories of meal
    /// </summary>
    public sealed class GetMealCategoriesResponse : IBaseResponse
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
        /// List of meal categories
        /// </summary>
        public IEnumerable<MealCategory> MealCategories { get; set; }
    }

    /// <summary>
    /// Internal class for meal categories
    /// </summary>
    public class MealCategory
    {
        /// <summary>
        /// Id of a meal category
        /// </summary>
        public int Id{ get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        /// <example> Dinner </example>
        public string Name{ get; set; }
    }
}
