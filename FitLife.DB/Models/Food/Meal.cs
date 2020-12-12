using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.DB.Models.Food
{
    public class Meal
    {
        public Meal()
        {
             MealProducts = new List<MealProduct>();    
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(64)")]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public bool Deleted { get; set; }

        public ICollection<MealProduct> MealProducts { get; set; }
        public ICollection<UserMeal> UserMeals { get; set; }


    }
}
