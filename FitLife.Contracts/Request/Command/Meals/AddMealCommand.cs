using System;
using System.Collections.Generic;
using System.Text;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Meals
{
    public class AddMealCommand : ICommand
    {
        public string Name { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
    }
}
