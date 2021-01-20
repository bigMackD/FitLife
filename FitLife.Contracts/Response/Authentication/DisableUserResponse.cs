using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Authentication
{
    public class DisableUserResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
