using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Products
{
    /// <summary>
    /// Command for addin a product
    /// </summary>
    public sealed class AddProductCommand : ICommand
    {
        /// <summary>
        /// Name of a product
        /// </summary>
        /// <example> Cheese </example>
        public string Name { get; set; }

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
