using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Meals
{
    /// <summary>
    /// Response for editing a meal
    /// </summary>
    public sealed class EditMealResponse : IBaseResponse
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
