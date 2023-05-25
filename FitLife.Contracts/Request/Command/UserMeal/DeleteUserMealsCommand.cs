using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.UserMeal
{
    /// <summary>
    /// Command for removing meals for a signed in user
    /// </summary>
    public sealed class DeleteUserMealsCommand : ICommand
    {
        /// <summary>
        /// Ids for meals of a user
        /// </summary>
        public List<int> Ids { get; set; }
    }
}
