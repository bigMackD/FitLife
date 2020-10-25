using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.DB.Models.Food
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal Calories { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal ProteinsGrams { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal CarbsGrams { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal FatsGrams { get; set; }

        public bool Deleted { get; set; }
    }
}
