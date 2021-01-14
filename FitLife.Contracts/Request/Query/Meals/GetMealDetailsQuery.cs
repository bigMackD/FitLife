using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Meals
{
    public class GetMealDetailsQuery : IQuery
    {
        public int Id { get; set; }
    }
}
