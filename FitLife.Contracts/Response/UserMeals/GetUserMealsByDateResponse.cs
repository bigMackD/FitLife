using System.Collections.Generic;
using FitLife.Contracts.Response.Meals;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.UserMeals
{
    public class GetUserMealsByDateResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }

        public IEnumerable<UserMealResponse> UserMeals { get; set; }
    }

    public class UserMealResponse : Meal
    {
        public int CategoryId { get; set; }
    }
}
