using System;
using System.Collections.Generic;
using System.Text;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Users
{
    public class GetUserProfileResponse : IBaseResponse
    {
        public string FullName { get; set; }

        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
