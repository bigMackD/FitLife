using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Products
{
    /// <summary>
    /// Query for retrieving products
    /// </summary>
    public sealed class GetProductsQuery : IQuery, IPagingQuery
    {
        /// <summary>
        /// Direction of sorting - DESC or ASC
        /// </summary>
        /// <example>DESC</example>
        public string SortDirection { get; set; }

        /// <summary>
        /// Index of a page
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int? PageSize { get; set; }
    }
}
