using System;
using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Users
{
    /// <summary>
    /// Response for retrieving users
    /// </summary>
    public sealed class GetUsersResponse : IBaseResponse, IPagingResponse
    {
        /// <summary>
        /// List of registered users
        /// </summary>
        public IEnumerable<User> Users { get; set; }
        
        /// <summary>
        /// Count of a registered users
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Indicates whether operation succeed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public string[] Errors { get; set; }
    }

    /// <summary>
    /// Internal class representing a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of a user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email address of a user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full name of a user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Indicates whether user is locked
        /// </summary>
        public bool Locked { get; set; }

    }
}
