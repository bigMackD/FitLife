using System.Collections.Generic;
using FitLife.Infrastructure.Models;

namespace FitLife.Infrastructure.Events
{
    public class ProcessPeriodicDietEvent
    {
        public string UserId { get; set; }

        public IEnumerable<DailyIntake> DailyIntake { get; set; }
    }
}
