using System;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastucture.CommandHandler;
using Microsoft.AspNetCore.Identity;

namespace FitLife.Infrastructure.CommandHandlers
{
    public class RegisterUserCommandHandler : IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private UserManager<AppUser> _userManager;
        public RegisterUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                    Success = false,
                    Errors = new[] {e.Message}
                };
            }
}
    }
}
