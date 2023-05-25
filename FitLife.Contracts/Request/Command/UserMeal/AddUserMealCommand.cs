using System;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.UserMeal
{
    /// <summary>
    /// Command for adding new meal for user
    /// </summary>
    public sealed class AddUserMealCommand : ICommand
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Id of the meal
        /// </summary>
        public int MealId { get; set; }
        
        /// <summary>
        /// Date of the meal consumed
        /// </summary>
        public DateTime ConsumedDate { get; set; }

        /// <summary>
        /// Id of the category
        /// </summary>
        public int CategoryId { get; set; }
    }
}
