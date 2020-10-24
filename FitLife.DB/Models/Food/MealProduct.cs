using System.ComponentModel.DataAnnotations;

namespace FitLife.DB.Models.Food
{
    public class MealProduct
    {

        [Key]
        public int Id { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
