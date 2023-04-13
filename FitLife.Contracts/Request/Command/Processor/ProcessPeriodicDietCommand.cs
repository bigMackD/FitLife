using System;
using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Processor
{
    /// <summary>
    /// Command for processing periodic diets for a user
    /// </summary>
    public sealed class ProcessPeriodicDietCommand : ICommand
    {
        /// <summary>
        /// Id of the event
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }
    }
}
