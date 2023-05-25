using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Products
{
    /// <summary>
    /// Query for retrieving details of a product
    /// </summary>
    public sealed class GetProductDetailsQuery : IQuery
    {
        /// <summary>
        /// Id of a product
        /// </summary>
        public int Id { get; set; }
    }
}
