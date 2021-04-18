using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.DB.Models.Food
{
    public class MealProduct
    {

        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealProductId { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public int ProductId { get; set; }
        public int Grams { get; set; }
        public Product Product { get; set; }
    }
}
