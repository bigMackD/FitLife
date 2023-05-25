using System;
using System.Collections.Generic;

namespace FitLife.Contracts.Events
{
    /// <summary>
    /// Class representing status of processed event
    /// </summary>
    public sealed class EventProcessed
    {
        /// <summary>
        /// Id of a event
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
