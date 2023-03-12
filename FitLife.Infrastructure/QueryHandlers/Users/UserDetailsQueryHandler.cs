using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Infrastructure.QueryHandlers.Users
{
    public class UserDetailsQueryHandler : IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public UserDetailsQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery query)
        {
            var user = await _userManager.Users.FirstAsync(appUser => appUser.Id == query.Id);

            return new UserDetailsResponse
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Success = true
            };
        }
    }
}
