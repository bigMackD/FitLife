using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Processor
{
    public class ProcessWeeklyDietCommand : ICommand
    {
        public string UserId { get; set; }
    }
}
