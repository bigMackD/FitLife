﻿using System.Threading.Tasks;
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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        private readonly IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> _registerUserCommandHandler;
        private readonly IAsyncCommandHandler<LoginUserCommand, LoginUserResponse> _loginUserCommandHandler;
        private readonly IAsyncQueryHandler<GetUsersQuery, GetUsersResponse> _getUsersQueryHandler;

        public UsersController(IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse> registerUserCommandHandler,
            IAsyncCommandHandler<LoginUserCommand, LoginUserResponse> loginUserCommandHandler,
            IAsyncQueryHandler<GetUsersQuery, GetUsersResponse> getUsersQueryHandler)
        {
            _registerUserCommandHandler = registerUserCommandHandler;
            _loginUserCommandHandler = loginUserCommandHandler;
            _getUsersQueryHandler = getUsersQueryHandler;
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

        [HttpGet]
        [AllowAuthorized(Role.Admin)]
        [Route("")]
        public Task<GetUsersResponse> Get()
        {
            //TODO REFACTOR
            var query = new GetUsersQuery();
            return _getUsersQueryHandler.Handle(query);
        }
    }
}
