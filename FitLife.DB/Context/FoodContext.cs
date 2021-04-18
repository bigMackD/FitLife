using FitLife.DB.Models.Food;
using Microsoft.EntityFrameworkCore;

namespace FitLife.DB.Context
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        public DbSet<MealProduct> MealProducts { get; set; }
        public DbSet<UserMeal> UserMeals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Meal>().ToTable("Meal");
            modelBuilder.Entity<Category>().ToTable("Category");


            modelBuilder.Entity<MealProduct>().ToTable("MealProduct")
                .HasKey(mp => mp.MealProductId);
            //.HasKey(mp => new { mp.MealId, mp.ProductId });
            modelBuilder.Entity<UserMeal>().ToTable("UserMeal")
                .HasKey(um => um.UserMealId);

            //.HasKey(um => new { um.UserId, um.MealId });

        }

    }
}
