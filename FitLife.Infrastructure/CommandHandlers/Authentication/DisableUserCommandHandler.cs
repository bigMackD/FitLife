using System;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Context;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Authentication
{
    public class DisableUserCommandHandler : IAsyncCommandHandler<DisableUserCommand, DisableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DisableUserCommandHandler> _logger;

        public DisableUserCommandHandler(IConfiguration configuration, ILogger<DisableUserCommandHandler> logger, AuthenticationContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<DisableUserResponse> Handle(DisableUserCommand command)
        {
            try
            {
                var user = await _context.AppUsers.FirstOrDefaultAsync(user => user.Id == command.Id);
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
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:Users:UserNotFound") }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new DisableUserResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
