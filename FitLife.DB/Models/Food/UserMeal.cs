using System;
using System.ComponentModel.DataAnnotations;
using FitLife.DB.Models.Authentication;

namespace FitLife.DB.Models.Food
{
    public class UserMeal
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
