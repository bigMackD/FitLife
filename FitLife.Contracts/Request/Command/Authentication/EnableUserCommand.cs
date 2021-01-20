using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    public class EnableUserCommand : ICommand
    {
        public string Id { get; set; }
    }
}
