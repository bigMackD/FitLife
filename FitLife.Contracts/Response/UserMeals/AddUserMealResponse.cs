using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.UserMeals
{
    /// <summary>
    /// Response for creating a meal
    /// </summary>
    public sealed class AddUserMealResponse : IBaseResponse
    {
        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }
    }
}
