using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    /// <summary>
    /// Response for retrieving product details
    /// </summary>
    public sealed class GetProductDetailsResponse : IBaseResponse
    {
        /// <summary>
        /// Product details
        /// </summary>
        public Product Product { get; set; }

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
