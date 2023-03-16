using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Command;
using Swashbuckle.AspNetCore.Annotations;

namespace FitLife.Contracts.Request.Command.Meals
{
    /// <summary>
    /// Command for adding new product
    /// </summary>
    public sealed class AddMealCommand : ICommand
    {

        /// <summary>
        /// The name of the meal.
        /// </summary>
        /// <example>Cacio e pepe</example>
        public string Name { get; set; }

        /// <summary>
        /// List of products of a meal
        /// </summary>
        public IEnumerable<AddMealProduct> MealProducts { get; set; }

        /// <summary>
        /// ID of the category
        /// </summary>
        /// <example>1</example>
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// Product for meal
    /// </summary>
    public class AddMealProduct
    {
        /// <summary>
        /// Id of the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Grams of a product
        /// </summary>
        public int Grams { get; set; }
    }
}
