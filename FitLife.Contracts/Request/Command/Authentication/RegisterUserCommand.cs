using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    /// <summary>
    /// Command for registering the user
    /// </summary>
    public sealed class RegisterUserCommand : ICommand
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

        /// <summary>
        /// Email address of a user
        /// </summary>
        /// <example>johndoe@mail.com</example>
        public string Email { get; set; }
        
        /// <summary>
        /// Full name of a user
        /// </summary>
        /// <example>John Doe</example>
        public string FullName { get; set; }
    }
}
