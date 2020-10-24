using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.DB.Models.Food
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Calories { get; set; }

        public int ProteinsGrams { get; set; }

        public int CarbsGrams { get; set; }

        public int FatsGrams { get; set; }

        public bool Deleted { get; set; }
    }
}
