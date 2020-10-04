using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.QueryHandlers.Users
{
    public class GetUserProfileQueryHandler : IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager, ILogger<GetUserProfileQueryHandler> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery query)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(query.UserId);
                return new GetUserProfileResponse
                {
                    Success = true,
                    FullName = user.FullName
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetUserProfileResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }

                };
            }
        }
    }
}
