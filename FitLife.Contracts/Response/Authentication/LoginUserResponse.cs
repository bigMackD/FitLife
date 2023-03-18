using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Authentication
{
    /// <summary>
    /// Response for user login
    /// </summary>
    public sealed class LoginUserResponse : IBaseResponse
    {
        /// <summary>
        /// Bearer token for logged in user
        /// </summary>
        public string Token { get; set; }

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
