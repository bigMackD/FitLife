using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FitLife.Infrastructure.CommandHandlers.Authentication
{
    public class LoginUserCommandHandler : IAsyncCommandHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;


        public LoginUserCommandHandler(UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
            {
                if (user.IsDisabled != null && user.IsDisabled.Value)
                {
                    return new LoginUserResponse
                    {
                        Errors = new[] { _configuration.GetValue<string>("Messages:Users:UserLocked") }
                    };
                }

                var roles = await _userManager.GetRolesAsync(user);
                IdentityOptions options = new IdentityOptions();
                var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTSecret"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                            new Claim("UserID", user.Id),
                            new Claim(options.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault())
                        }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return new LoginUserResponse
                {
                    Success = true,
                    Token = token
                };
            }

            return new LoginUserResponse
            {
                Errors = new[] { _configuration.GetValue<string>("Messages:Users:PasswordIncorrect") }
            };
        }
    }
}
