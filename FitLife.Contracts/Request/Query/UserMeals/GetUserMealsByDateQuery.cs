using System;
using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.UserMeals
{
    public class GetUserMealsByDateQuery 
    {
        public DateTime Date { get; set; }
    }
    public class GetUserMealsByDateInternalQuery :  GetUserMealsByDateQuery, IQuery
    {
        public string Id { get; set; }
    }
}
