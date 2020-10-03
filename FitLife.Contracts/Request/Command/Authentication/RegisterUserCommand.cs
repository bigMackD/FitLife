using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
