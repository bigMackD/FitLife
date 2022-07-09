using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitLife.DB.Models.Authentication;

namespace FitLife.DB.Models.Diet
{
    public class UserPeriodicDiet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal Calories { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal ProteinsGrams { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal CarbsGrams { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal FatsGrams { get; set; }

        [Column(TypeName = "date")]
        public DateTime PeriodStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime PeriodEnd { get; set; }
    }
}
