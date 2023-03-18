using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Meals
{
    /// <summary>
    /// Command for deleting a meal
    /// </summary>
    public sealed class DeleteMealCommand : ICommand
    {
        /// <summary>
        /// Id of a meal to be deleted
        /// </summary>
        public int Id { get; set; }
    }
}
