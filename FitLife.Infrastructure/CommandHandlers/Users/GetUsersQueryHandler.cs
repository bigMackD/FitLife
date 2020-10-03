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

namespace FitLife.Infrastructure.CommandHandlers.Users
{
    public class GetUsersQueryHandler : IAsyncQueryHandler<GetUsersQuery,GetUsersResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public GetUsersQueryHandler(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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
                    FullName = user.FullName,
                    UserName = user.UserName
                });

                return new GetUsersResponse
                {
                    Success = true,
                    Users = response
                };
            }
            catch (Exception e)
            {
                return new GetUsersResponse
                {
                    //TODO Logger
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
