using System.Collections.Generic;
using FitLife.Contracts.Events.Base;
using FitLife.Contracts.Models;

namespace FitLife.Contracts.Events
{
    /// <summary>
    /// Event for calulating user periodic diet
    /// </summary>
    public class ProcessPeriodicDietEvent : BaseEvent
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// List of daily intakes
        /// </summary>
        public IEnumerable<DailyIntake> DailyIntake { get; set; }
    }
}
