using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastucture.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FitLife.Infrastructure.CommandHandlers.Authentication
{
    public class RegisterUserCommandHandler : IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<RegisterUserResponse> Handle(RegisterUserCommand command)
        {
            try
            {
                var appUser = new AppUser()
                {
                    UserName = command.UserName,
                    Email = command.Email,
                    FullName = command.FullName
                };

                var result = await _userManager.CreateAsync(appUser, command.Password);
                await _userManager.AddToRoleAsync(appUser, Role.User.ToString());
                return new RegisterUserResponse
                {
                    Success = result.Succeeded,
                    Errors = result.Errors.Select(x => x.Description).ToArray()
                };
            }
            catch (Exception e)
            {
                return new RegisterUserResponse
                {
                    //TODO Logger
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }

                };
            }
}
    }
}
