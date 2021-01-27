using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Authentication
{
    public class EnableUserCommandHandler : IAsyncCommandHandler<EnableUserCommand, EnableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnableUserCommandHandler> _logger;
        private readonly UserManager<AppUser> _userManager;

        public EnableUserCommandHandler(IConfiguration configuration, ILogger<EnableUserCommandHandler> logger, AuthenticationContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<EnableUserResponse> Handle(EnableUserCommand command)
        {
            try
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
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:Users:UserNotFound") }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new EnableUserResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
