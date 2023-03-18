using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Processor
{
    /// <summary>
    /// Response of processing periodic diet
    /// </summary>
    public sealed class ProcessPeriodicDietResponse : IBaseResponse
    {
        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }
    }
}
