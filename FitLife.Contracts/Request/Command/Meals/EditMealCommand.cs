using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Meals
{
    /// <summary>
    /// Command for editing a meal
    /// </summary>
    public sealed class EditMealCommand : ICommand
    {
        /// <summary>
        /// Id of a meal to be edited
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the meal
        /// </summary>
        /// <example>Cacio e pepe</example>
        public string Name { get; set; }
        
        /// <summary>
        /// List of a products for a meal
        /// </summary>
        public IEnumerable<EditMealProduct> MealProducts { get; set; }
        
        /// <summary>
        /// Id of the category
        /// </summary>
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// Products for a meal
    /// </summary>
    public class EditMealProduct
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
