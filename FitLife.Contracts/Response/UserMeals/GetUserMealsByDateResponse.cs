using System.Collections.Generic;
using FitLife.Contracts.Response.Meals;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.UserMeals
{
    /// <summary>
    /// Response for retrieving meals for a user by date 
    /// </summary>
    public sealed class GetUserMealsByDateResponse : IBaseResponse
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
        /// List of a meals for a user
        /// </summary>
        public IEnumerable<UserMealResponse> UserMeals { get; set; }
    }

    /// <summary>
    /// Internal class for user meal
    /// </summary>
    public class UserMealResponse : Meal
    {
        /// <summary>
        /// Id of a category for a meal
        /// </summary>
        public int CategoryId { get; set; }
    }
}
