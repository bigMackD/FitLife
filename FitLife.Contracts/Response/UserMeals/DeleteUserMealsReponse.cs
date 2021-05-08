using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.UserMeals
{
    public class DeleteUserMealsReponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
