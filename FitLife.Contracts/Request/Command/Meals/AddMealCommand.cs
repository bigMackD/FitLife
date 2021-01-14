using System;
using System.Collections.Generic;
using System.Text;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Meals
{
    public class AddMealCommand : ICommand
    {
        public string Name { get; set; }
        public IEnumerable<AddMealProduct> MealProducts { get; set; }
        public int CategoryId { get; set; }
    }

    public class AddMealProduct
    {
        public int Id { get; set; }
        public int Grams { get; set; }
    }
}
