using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    public class AddProductResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
