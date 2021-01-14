using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    public class EditProductResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
