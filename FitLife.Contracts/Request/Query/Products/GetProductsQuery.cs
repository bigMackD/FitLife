using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Products
{
    public class GetProductsQuery : IQuery, IPagingQuery
    {
        public string SortDirection { get; set; }
        public int PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
