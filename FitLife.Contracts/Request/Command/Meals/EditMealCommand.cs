using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Meals
{
    public class EditMealCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EditMealProduct> MealProducts { get; set; }
        public int CategoryId { get; set; }
    }

    public class EditMealProduct
    {
        public int Id { get; set; }
        public int Grams { get; set; }
    }
}
