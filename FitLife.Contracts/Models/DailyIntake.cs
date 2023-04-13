using System;

namespace FitLife.Contracts.Models
{
    public class DailyIntake
    {
        public DateTime Date { get; set; }

        public decimal ProteinsGrams { get; set; }

        public decimal CarbsGrams { get; set; }

        public decimal FatsGrams { get; set; }
    }
}
