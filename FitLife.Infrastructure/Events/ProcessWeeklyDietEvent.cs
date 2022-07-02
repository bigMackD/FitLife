using System.Collections.Generic;

namespace FitLife.Infrastructure.Events
{
    public class ProcessWeeklyDietEvent
    {
        public string UserId { get; set; }

        public IEnumerable<float> DailyIntake { get; set; }
    }
}
