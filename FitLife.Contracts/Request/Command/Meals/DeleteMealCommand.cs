using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Meals
{
    public class DeleteMealCommand : ICommand
    {
        public int Id { get; set; }
    }
}
