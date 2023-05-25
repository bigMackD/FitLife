using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    /// <summary>
    /// Response of edit of a product
    /// </summary>
    public sealed class EditProductResponse : IBaseResponse
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
