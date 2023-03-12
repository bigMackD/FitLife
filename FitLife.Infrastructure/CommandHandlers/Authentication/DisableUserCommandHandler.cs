using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Authentication
{
    public class DisableUserCommandHandler : IAsyncCommandHandler<DisableUserCommand, DisableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public DisableUserCommandHandler(IConfiguration configuration, AuthenticationContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public async Task<DisableUserResponse> Handle(DisableUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            if (user != null)
            {
                user.IsDisabled = true;
                await _context.SaveChangesAsync();

                return new DisableUserResponse
                {
                    Success = true
                };
            }

            return new DisableUserResponse
            {
                Errors = new[] { _configuration.GetValue<string>("Messages:Users:UserNotFound") }
            };
        }
    }
}
