using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Authentication
{
    /// <summary>
    /// Response for user register
    /// </summary>
    public sealed class RegisterUserResponse : IBaseResponse
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
