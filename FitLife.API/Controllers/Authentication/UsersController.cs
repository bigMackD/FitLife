using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Authentication;
using FitLife.Contracts.Response.Users;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.Extensions;
using FitLife.Shared.Infrastructure.QueryHandler;
using FitLife.Shared.Infrastucture.Enum;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Authentication
{
    /// <summary>
    /// Controller for managing users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        private readonly IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> _registerUserCommandHandler;
        private readonly IAsyncCommandHandler<LoginUserCommand, LoginUserResponse> _loginUserCommandHandler;
        private readonly IAsyncQueryHandler<GetUsersQuery, GetUsersResponse> _getUsersQueryHandler;
        private readonly IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse> _userDetailsQueryHandler;
        private readonly IAsyncCommandHandler<DisableUserCommand, DisableUserResponse> _disableUserCommandHandler;
        private readonly IAsyncCommandHandler<EnableUserCommand, EnableUserResponse> _enableUserCommandHandler;

        /// <param name="registerUserCommandHandler"></param>
        /// <param name="loginUserCommandHandler"></param>
        /// <param name="getUsersQueryHandler"></param>
        /// <param name="userDetailsQueryHandler"></param>
        /// <param name="disableUserCommandHandler"></param>
        /// <param name="enableUserCommandHandler"></param>
        public UsersController(IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> registerUserCommandHandler,
            IAsyncCommandHandler<LoginUserCommand, LoginUserResponse> loginUserCommandHandler,
            IAsyncQueryHandler<GetUsersQuery, GetUsersResponse> getUsersQueryHandler,
            IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse> userDetailsQueryHandler,
            IAsyncCommandHandler<DisableUserCommand, DisableUserResponse> disableUserCommandHandler,
            IAsyncCommandHandler<EnableUserCommand, EnableUserResponse> enableUserCommandHandler)
        {
            _registerUserCommandHandler = registerUserCommandHandler;
            _loginUserCommandHandler = loginUserCommandHandler;
            _getUsersQueryHandler = getUsersQueryHandler;
            _userDetailsQueryHandler = userDetailsQueryHandler;
            _disableUserCommandHandler = disableUserCommandHandler;
            _enableUserCommandHandler = enableUserCommandHandler;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">User sucessfully created</response>
        [HttpPost]
        [Route("Register")]
        public Task<RegisterUserResponse> Register(RegisterUserCommand command)
        {
            return _registerUserCommandHandler.Handle(command);
        }

        /// <summary>
        /// Logins the user
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">User sucessfully logged in</response>
        /// <returns>Bearer token</returns>
        [HttpPost]
        [Route("Login")]
        public Task<LoginUserResponse> Login(LoginUserCommand command)
        {
            return _loginUserCommandHandler.Handle(command);
        }

        /// <summary>
        /// Returns all registered users
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">List of registered users</response>
        [HttpGet]
        [AllowAuthorized(Role.Admin)]
        [Route("")]
        public Task<GetUsersResponse> Get([FromQuery]GetUsersQuery query)
        {
            return _getUsersQueryHandler.Handle(query);
        }

        /// <summary>
        /// Returns user by specified ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">User details</response>
        [HttpGet]
        [AllowAuthorized(Role.Admin)]
        [Route("{id}")]
        public Task<UserDetailsResponse> Get([FromRoute] GetUserDetailsQuery query)
        {
            return _userDetailsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Disables users account by specified ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">User disabled</response>
        [HttpGet]
        [AllowAuthorized(Role.Admin)]
        [Route("disable/{id}")]
        public Task<DisableUserResponse> Disable([FromRoute] DisableUserCommand query)
        {
            return _disableUserCommandHandler.Handle(query);
        }
        
        /// <summary>
        /// Enables users account by specified ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">User disabled</response>
        [HttpGet]
        [AllowAuthorized(Role.Admin)]
        [Route("enable/{id}")]
        public Task<EnableUserResponse> Enable([FromRoute] EnableUserCommand query)
        {
            return _enableUserCommandHandler.Handle(query);
        }
    }
}
