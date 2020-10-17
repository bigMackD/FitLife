using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Users
{
    public class UserDetailsResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
