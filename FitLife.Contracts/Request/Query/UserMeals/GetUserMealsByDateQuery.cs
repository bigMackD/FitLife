using System;
using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.UserMeals
{
    /// <summary>
    /// Query for retrieving meals for a signed in user by date
    /// </summary>
    public  class GetUserMealsByDateQuery 
    {
        /// <summary>
        /// Date of logged meals
        /// </summary>
        public DateTime Date { get; set; }
    }
    /// <summary>
    /// Internal query for retrieving meals for a signed in user by date
    /// </summary>
    public sealed class GetUserMealsByDateInternalQuery :  GetUserMealsByDateQuery, IQuery
    {
        /// <summary>
        /// Id of a user
        /// </summary>
        public string Id { get; set; }
    }
}
