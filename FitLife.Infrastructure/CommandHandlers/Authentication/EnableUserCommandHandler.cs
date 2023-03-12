using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.CommandHandlers.Authentication
{
    public class EnableUserCommandHandler : IAsyncCommandHandler<EnableUserCommand, EnableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public EnableUserCommandHandler(IConfiguration configuration, AuthenticationContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public async Task<EnableUserResponse> Handle(EnableUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            if (user != null)
            {
                user.IsDisabled = false;
                await _context.SaveChangesAsync();

                return new EnableUserResponse
                {
                    Success = true
                };
            }

            return new EnableUserResponse
            {
                Errors = new[] { _configuration.GetValue<string>("Messages:Users:UserNotFound") }
            };
        }
    }
}
