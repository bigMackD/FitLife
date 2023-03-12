using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Infrastructure.QueryHandlers.Users
{
    public class GetUsersQueryHandler : IAsyncQueryHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery query)
        {
            //TODO
            var users = await _userManager.Users.ToListAsync();
            var response = users
                .OrderBy(user => user.FullName)
                .Skip((query.PageIndex) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .Select(user => new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Locked = user.IsDisabled != null && user.IsDisabled.Value
                });

            return new GetUsersResponse
            {
                Success = true,
                Users = response,
                Count = users.Count
            };
        }
    }
}
