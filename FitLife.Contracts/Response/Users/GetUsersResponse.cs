using System;
using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Users
{
    public class GetUsersResponse : IBaseResponse, IPagingResponse
    {
        public IEnumerable<User> Users { get; set; }
        public int Count { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Locked { get; set; }

    }
}
