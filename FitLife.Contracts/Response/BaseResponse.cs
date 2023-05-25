using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response
{
    /// <summary>
    /// Base of all responses
    /// </summary>
    public sealed class BaseResponse : IBaseResponse
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
