using FitLife.DB.Models.Diet;
using Microsoft.EntityFrameworkCore;

namespace FitLife.DB.Context
{
    public class DietContext : DbContext
    {
        public DietContext(DbContextOptions<DietContext> options) : base(options)
        {
        }
        public DbSet<UserPeriodicDiet> UserPeriodicDiets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPeriodicDiet>().ToTable("UserPeriodicDiet");

        }
    }
}
