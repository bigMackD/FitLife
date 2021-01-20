using System;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Authentication
{
    public class DisableUserCommand : ICommand
    {
        public string Id { get; set; }
    }
}
