using FitLife.Shared.Infrastucture.Response;

namespace FitLife.Contracts.Response.Authentication
{
    public class LoginUserResponse : IBaseResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
