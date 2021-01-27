using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.UserMeals
{
    public class AddUserMealResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
