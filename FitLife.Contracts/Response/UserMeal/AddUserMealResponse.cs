using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.UserMeal
{
    public class AddUserMealResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
