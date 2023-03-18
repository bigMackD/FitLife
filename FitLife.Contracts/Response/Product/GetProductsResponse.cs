using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    /// <summary>
    /// Response for retrieving products
    /// </summary>
    public sealed class GetProductsResponse : IBaseResponse, IPagingResponse
    {
     
        /// <summary>
        /// List of products
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        
        /// <summary>
        /// Count of all products
        /// </summary>
        public int Count { get; set; }

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
    /// Product response
    /// </summary>
    public class Product
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
