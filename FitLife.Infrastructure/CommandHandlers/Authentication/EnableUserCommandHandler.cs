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
    public class EnableUserCommandHandler : IAsyncCommandHandler<EnableUserCommand, EnableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnableUserCommandHandler> _logger;

        public EnableUserCommandHandler(IConfiguration configuration, ILogger<EnableUserCommandHandler> logger, AuthenticationContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<EnableUserResponse> Handle(EnableUserCommand command)
        {
            try
            {
                var user = await _context.AppUsers.FirstOrDefaultAsync(user => user.Id == command.Id);
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
