using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Meals
{
    /// <summary>
    /// Query for retrieving meal details 
    /// </summary>
    public sealed class GetMealDetailsQuery : IQuery
    {
        /// <summary>
        /// Id of the meal
        /// </summary>
        public int Id { get; set; }
    }
}
