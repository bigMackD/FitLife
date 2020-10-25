using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Products
{
   public class GetProductDetailsQuery : IQuery
    {
        public int Id { get; set; }
    }
}
