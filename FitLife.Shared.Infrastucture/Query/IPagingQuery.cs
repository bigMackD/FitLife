namespace FitLife.Shared.Infrastructure.Query
{
    public interface IPagingQuery
    {
         string SortDirection { get; set; }
         int PageIndex { get; set; }
         int? PageSize { get; set; }
    }
}
