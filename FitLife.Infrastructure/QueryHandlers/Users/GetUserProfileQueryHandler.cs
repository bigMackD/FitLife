using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Identity;

namespace FitLife.Infrastructure.QueryHandlers.Users
{
    public class GetUserProfileQueryHandler : IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery query)
        {
            var user = await _userManager.FindByIdAsync(query.UserId);
            return new GetUserProfileResponse
            {
                Success = true,
                FullName = user.FullName
            };
        }
    }
}
