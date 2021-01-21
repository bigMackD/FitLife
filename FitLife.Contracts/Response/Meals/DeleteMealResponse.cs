using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Meals
{
    public class DeleteMealResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
