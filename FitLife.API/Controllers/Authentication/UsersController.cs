using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Response.Authentication;
using FitLife.Shared.Infrastucture.CommandHandler;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        private readonly IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> _registerUserCommandHandler;
        private readonly IAsyncCommandHandler<LoginUserCommand, LoginUserResponse> _loginUserCommandHandler;

        public UsersController(IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> registerUserCommandHandler,
            IAsyncCommandHandler<LoginUserCommand, LoginUserResponse> loginUserCommandHandler)
        {
            _registerUserCommandHandler = registerUserCommandHandler;
            _loginUserCommandHandler = loginUserCommandHandler;
        }

        [HttpPost]
        [Route("Register")]
        public Task<RegisterUserResponse> Register(RegisterUserCommand command)
        {
            return _registerUserCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("Login")]
        public Task<LoginUserResponse> Login(LoginUserCommand command)
        {
            return _loginUserCommandHandler.Handle(command);
        }
    }
}
