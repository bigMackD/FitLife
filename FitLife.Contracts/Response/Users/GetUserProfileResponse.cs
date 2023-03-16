using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Users
{
    /// <summary>
    /// Response for a signed in user
    /// </summary>
    public sealed class GetUserProfileResponse : IBaseResponse
    {
        /// <summary>
        /// Full name of a signed in user
        /// </summary>
        public string FullName { get; set; }

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
