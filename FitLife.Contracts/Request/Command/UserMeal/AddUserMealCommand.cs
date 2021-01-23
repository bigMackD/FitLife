using System;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.UserMeal
{
    public class AddUserMealCommand : ICommand
    {
        public string UserId { get; set; }
        public int MealId { get; set; }
        public DateTime ConsumedDate { get; set; }
        public int CategoryId { get; set; }
    }
}
