using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    /// <summary>
    /// Response for adding products
    /// </summary>
    public sealed class AddProductResponse : IBaseResponse
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
