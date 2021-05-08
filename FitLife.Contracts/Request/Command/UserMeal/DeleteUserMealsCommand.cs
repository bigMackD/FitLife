using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.UserMeal
{
    public class DeleteUserMealsCommand : ICommand
    {
        public List<int> Ids { get; set; }
    }
}
