using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FitLife.DB.Models.Food;
using Microsoft.AspNetCore.Identity;

namespace FitLife.DB.Models.Authentication
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public ICollection<UserMeal> UserMeals { get; set; }
    }
}
