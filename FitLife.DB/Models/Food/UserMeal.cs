using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitLife.DB.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FitLife.DB.Models.Food
{
    public class UserMeal
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public DateTime ConsumedDate { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
