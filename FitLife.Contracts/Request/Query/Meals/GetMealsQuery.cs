using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Meals
{
    /// <summary>
    /// Query for retrieving meals
    /// </summary>
    public sealed class GetMealsQuery : IQuery, IPagingQuery
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
