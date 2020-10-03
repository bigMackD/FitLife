using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    public class LoginUserCommand : ICommand
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
