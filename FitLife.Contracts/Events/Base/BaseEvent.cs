using System;

namespace FitLife.Contracts.Events.Base
{
    /// <summary>
    /// Base of all events
    /// </summary>
    public class BaseEvent
    {
        /// <summary>
        /// Id of an event
        /// </summary>
        public Guid Id { get; set; }
    }
}
