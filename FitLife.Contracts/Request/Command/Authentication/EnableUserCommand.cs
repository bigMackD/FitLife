using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    /// <summary>
    /// Command for disabling user
    /// </summary>
    public sealed class EnableUserCommand : ICommand
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string Id { get; set; }
    }
}
