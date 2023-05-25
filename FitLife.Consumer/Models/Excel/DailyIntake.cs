using System.ComponentModel;

namespace FitLife.Consumer.Models.Excel
{
    public sealed class DailyIntake
    {
        [DisplayName("Date")]
        public DateOnly Date { get; set; }

        [DisplayName("Carbohydrates consumed")]
        public decimal CarbsGrams { get; set; }

        [DisplayName("Proteins consumed")]
        public decimal ProteinsGrams { get; set; }

        [DisplayName("Fats consumed")]
        public decimal FatsGrams { get; set; }

        [DisplayName("Calories consumed")]
        public decimal Calories { get; set; }
    }
}
