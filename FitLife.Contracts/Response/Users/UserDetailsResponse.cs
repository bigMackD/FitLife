using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Users
{
    /// <summary>
    /// Response of details of a user
    /// </summary>
    public sealed class UserDetailsResponse : IBaseResponse
    {
        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }

        /// <summary>
        /// Email address of a user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full name of a user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Name on which user registered
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Phone number of a user
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Indicates whether user confirmed his phone number
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Indicates whether user enabled two factor authentication
        /// </summary>
        public bool TwoFactorEnabled { get; set; }
    }
}
