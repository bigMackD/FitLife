using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.DB.Models.Food
{
    public class Product
    {
        public Product()
        {
            MealProducts = new List<MealProduct>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(32)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal Calories { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal ProteinsGrams { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal CarbsGrams { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal FatsGrams { get; set; }

        public bool Deleted { get; set; }

        public ICollection<MealProduct> MealProducts { get; set; }

    }
}
