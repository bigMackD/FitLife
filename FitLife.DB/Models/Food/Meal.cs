using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.DB.Models.Food
{
    public class Meal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Calories { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public bool Deleted { get; set; }

        public ICollection<MealProduct> MealProducts { get; set; }
        public ICollection<UserMeal> UserMeals { get; set; }


    }
}
