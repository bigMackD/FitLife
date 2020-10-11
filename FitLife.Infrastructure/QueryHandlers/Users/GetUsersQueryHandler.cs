using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.QueryHandlers.Users
{
    public class GetUsersQueryHandler : IAsyncQueryHandler<GetUsersQuery,GetUsersResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public GetUsersQueryHandler(UserManager<AppUser> userManager, IConfiguration configuration, ILogger<GetUsersQueryHandler> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery query)
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var response = users.Select(user => new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName
                });

                return new GetUsersResponse
                {
                    Success = true,
                    Users = response
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return new GetUsersResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
