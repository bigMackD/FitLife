using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    /// <summary>
    /// Command for logging in the user
    /// </summary>
    public sealed class LoginUserCommand : ICommand
    {
        /// <summary>
        /// User name
        /// </summary>
        /// <example>TestUser</example>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>TestPassword123!</example>
        public string Password { get; set; }
    }
}
